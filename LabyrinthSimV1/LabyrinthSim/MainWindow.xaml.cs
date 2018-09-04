using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabyrinthSim
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int interval = 500;

        private bool drive;
        private int wait;
        private Timer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new Timer(interval);
            timer.Elapsed += Timer_Elapsed;

            tbxWait.Text = interval.ToString();
        }

        private void NextLabyrinthStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] labyrinthData = File.ReadAllText("Data.txt").Split(';');
                int width = int.Parse(labyrinthData.First());
                int height = int.Parse(labyrinthData.ElementAt(1));
                Labyrinth labyrinth = Labyrinth.GetActual(width, height);

                foreach (string relationData in labyrinthData.Skip(2))
                {
                    double[] relationPosition = relationData.Split('x').Select(d => double.Parse(d)).ToArray();

                    labyrinth[relationPosition[0], relationPosition[1]].Close();
                }

                lc.Robot.ActualPossibleRoute?.Cancel();
                lc.Robot.LearnMaybeRoute?.Cancel();
                lc.Robot.LearnPossibleRoute?.Cancel();
                lc.Robot.LearnRobotRoute?.Cancel();

                lc.Robot = new Robot(labyrinth);
            }
            catch { }
        }

        private void NextRobotStep_Click(object sender, RoutedEventArgs e)
        {
            lc.InvalidateVisual();
            lc.Robot.NextStep();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lc.Robot.ActualPossibleRoute?.Cancel();
            lc.Robot.LearnMaybeRoute?.Cancel();
            lc.Robot.LearnPossibleRoute?.Cancel();
            lc.Robot.LearnRobotRoute?.Cancel();

            lc.LabyrinthControl_MouseLeftButtonUp(lc, e);
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            lc.Robot.ActualPossibleRoute?.Cancel();
            lc.Robot.LearnMaybeRoute?.Cancel();
            lc.Robot.LearnPossibleRoute?.Cancel();
            lc.Robot.LearnRobotRoute?.Cancel();

            lc.LabyrinthControl_MouseRightButtonUp(lc, e);
        }

        private void SaveLabyrinth_Click(object sender, RoutedEventArgs e)
        {
            Labyrinth lab = lc.Robot.ActualLabyrinth;
            string data = string.Join(";", lab.GetAllRelations().Where(r => r.Relation == RelationType.Close));
            File.WriteAllText("Data.txt", lab.Width + ";" + lab.Height + ";" + data);
        }

        private void TimerDrive_Checked(object sender, RoutedEventArgs e)
        {
            //cbxFullSpeedDrive.IsChecked = false;
            //wait = lc.Wait;
            //lc.Wait = 0;

            //timer.Start();
        }

        private void TimerDrive_Unchecked(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lc.Robot.NextStep();
        }

        private void FullSpeed_Checked(object sender, RoutedEventArgs e)
        {
            StartFullSpeedDrive();
        }

        private void FullSpeed_Unchecked(object sender, RoutedEventArgs e)
        {
            drive = false;
        }

        private void StartFullSpeedDrive()
        {
            cbxTimerDrive.IsChecked = false;
            drive = true;

            lc.Wait = wait;

            new Task(new Action(FullSpeedDrive)).Start();
        }

        private void FullSpeedDrive()
        {
            Labyrinth actulLab = lc.Robot.ActualLabyrinth;
            lc.Robot.ActualPossibleRoute.SearchPossibleAsync(actulLab[0, 0], null, actulLab.Target);
            while (drive && !lc.Robot.ActualPossibleRoute.Finished) lc.Robot.ActualPossibleRoute.Task?.Wait(100);

            while (drive)
            {
                lc.Robot.NextStep();
                Task.Delay(wait).Wait(wait + 10);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = (sender as TextBox)?.Text;

            if (s != null) int.TryParse(s, out wait);
        }
    }
}
