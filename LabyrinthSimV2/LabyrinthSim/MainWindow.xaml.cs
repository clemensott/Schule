using LabyrinthSim.PresentationSetups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabyrinthSim
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int interval = 500;

        public MainWindow()
        {
            InitializeComponent();

            lbxSetups.ItemsSource = PresentationSetup.GetSetups();
            if (lbxSetups.Items.Count > 0) lbxSetups.SelectedItem = lbxSetups.Items[0];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResizeLabyrinthControl();
        }

        public void LabyrinthControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lc.Labyrinth == null) return;

            Point position = e.GetPosition(lc);

            double widthFactor = lc.ActualWidth / lc.BlocksWidth;
            double heightFactor = lc.ActualHeight / lc.BlocksHeight;
            position = new Point(position.X / widthFactor - 0.5, position.Y / heightFactor - 0.5);

            int relationValue = lc.Labyrinth[position.X, position.Y];

            if (relationValue == 0) lc.Labyrinth[position.X, position.Y] = 1;
            else if (relationValue == 1) lc.Labyrinth[position.X, position.Y] = 0;
            else if (relationValue == 2) lc.Labyrinth[position.X, position.Y] = 3;
            else if (relationValue == 3) lc.Labyrinth[position.X, position.Y] = 2;

            lc.HasChanges = true;
        }

        public void LabyrinthControl_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lc.Labyrinth == null) return;

            Point position = e.GetPosition(lc);

            double widthFactor = lc.ActualWidth / lc.BlocksWidth;
            double heightFactor = lc.ActualHeight / lc.BlocksHeight;

            int x = Convert.ToInt32(Math.Round(position.X / widthFactor - 0.5));
            int y = Convert.ToInt32(Math.Round(position.Y / heightFactor - 0.5));

            lc.Labyrinth.Target = new SquareTarget(new Block(x, y));
            //lc.Robot = new Robot(lc.Labyrinth);

            lc.HasChanges = true;
        }

        private void LoadLabyrinth_Click(object sender, RoutedEventArgs e)
        {
            LoadLabyrinth();

            lc.IsViewVirtual = false;
            lc.Robot = new Robot(lc.Labyrinth);

            Waiter waiter = new Waiter()
            {
                Time = TimeSpan.Zero,
                Pause = true
            };

            SearchView search = new SearchView()
            {
                Labyrinth = lc.Labyrinth,
                Target = lc.Labyrinth.Target,
                Start = Block.Origin,
                IsVisible = true,
                SearchType = SearchType.Direct,
                IsDistancesVisable = true,
                IsCountsVisable = true,
                IsLengthVisable = true,
                InterpreterType = RelationInterpreterType.Actual,
                TryAddWaiter = waiter
            };

            lc.Searches.Add(search);

        }

        private void LoadLabyrinth()
        {
            try
            {
                string[] generalParts = File.ReadAllText("Data.txt").Split('|');

                string[] sizeParts = generalParts[0].Split('x');
                int width = int.Parse(sizeParts[0]);
                int height = int.Parse(sizeParts[1]);

                string[] targetParts = generalParts[1].Split('x');
                int targetX = int.Parse(targetParts[0]);
                int targetY = int.Parse(targetParts[1]);

                Labyrinth lab = new Labyrinth(width, height, targetX, targetY);

                foreach (string hClosedText in generalParts[2].Split(';'))
                {
                    string[] hClosedParts = hClosedText.Split('x');
                    int x = int.Parse(hClosedParts[0]);
                    int y = int.Parse(hClosedParts[1]);

                    lab.H[x, y] = 3;
                }

                foreach (string vClosedText in generalParts[3].Split(';'))
                {
                    string[] vClosedParts = vClosedText.Split('x');
                    int x = int.Parse(vClosedParts[0]);
                    int y = int.Parse(vClosedParts[1]);

                    lab.V[x, y] = 3;
                }

                lc.Labyrinth = lab;
            }
            catch { }
        }

        private void SaveLabyrinth_Click(object sender, RoutedEventArgs e)
        {
            IRelationInterpreter interpreter = new ActualInterpreter();
            Labyrinth lab = lc.Labyrinth;

            string text = lab.Width + "x" + lab.Height + "|";
            text += lab.Target.TopLeft.X + "x" + lab.Target.TopLeft.Y + "|";
            text += string.Join(";", GetClosedRealtions(lab.H)) + "|";
            text += string.Join(";", GetClosedRealtions(lab.V));

            try
            {
                File.WriteAllText("Data.txt", text);
            }
            catch { }
        }

        private IEnumerable<string> GetClosedRealtions(RelationArray array)
        {
            IRelationInterpreter interpreter = new ActualInterpreter();

            for (int i = 0; i < array.Width; i++)
            {
                for (int j = 0; j < array.Height; j++)
                {
                    if (!interpreter.IsOpen(array[i, j])) yield return i + "x" + j;
                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(ActualWidth + " x " + ActualHeight);
        }

        private void GridSplitter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResizeLabyrinthControl();
        }

        private void ResizeLabyrinthControl()
        {
            if (lc.BlocksWidth == 0 || lc.BlocksHeight == 0) return;

            lcColumn.Width = GridLength.Auto;
            lc.Width = lc.ActualHeight / lc.BlocksHeight * lc.BlocksWidth;
        }

        private void BtnPreviousSetup_Click(object sender, RoutedEventArgs e)
        {
            if (lbxSetups.Items.Count < 1) return;

            lbxSetups.SelectedIndex = (lbxSetups.SelectedIndex - 1 + lbxSetups.Items.Count) % lbxSetups.Items.Count;
        }

        private void BtnPreviousSub_Click(object sender, RoutedEventArgs e)
        {
            if (lbxSetups.SelectedItem is PresentationSetup setup) setup.Previous(lc);
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            if (lbxSetups.Items.Count < 1 || lbxSetups.SelectedItem == null) return;

            (lbxSetups.SelectedItem as PresentationSetup)?.Setup(lc);
        }

        private void BtnNextSub_Click(object sender, RoutedEventArgs e)
        {
            if (lbxSetups.SelectedItem is PresentationSetup setup) setup.Next(lc);
        }

        private void BtnNextSetup_Click(object sender, RoutedEventArgs e)
        {
            if (lbxSetups.Items.Count < 1) return;

            lbxSetups.SelectedIndex = (lbxSetups.SelectedIndex + 1) % lbxSetups.Items.Count;
        }

        private void LbxSetups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0 && e.RemovedItems.Count > 0)
            {
                lbxSetups.SelectedItem = e.RemovedItems[0];
            }
            else if (e.AddedItems.Count > 0)
            {
                e.AddedItems.OfType<PresentationSetup>().First().Setup(lc);
            }
            else return;
        }

        private void BtnAddSearch_Click(object sender, RoutedEventArgs e)
        {
            lc.Searches.Add(new SearchView());
        }

        private void BtnRemoveSearch_Click(object sender, RoutedEventArgs e)
        {
            if (lbxSearches.SelectedItem is SearchView search) lc.Searches.Remove(search);
        }

        private void BtnAddRobotSearches_Click(object sender, RoutedEventArgs e)
        {
            if (!lc.Searches.Contains(lc.Robot.BestSearch)) lc.Searches.Add(lc.Robot.BestSearch);
            if (!lc.Searches.Contains(lc.Robot.WorstSearch)) lc.Searches.Add(lc.Robot.WorstSearch);
            if (!lc.Searches.Contains(lc.Robot.DriveSearch)) lc.Searches.Add(lc.Robot.DriveSearch);
        }
    }
}
