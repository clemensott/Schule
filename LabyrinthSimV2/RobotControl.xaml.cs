using System.Windows;
using System.Windows.Controls;

namespace LabyrinthSim
{
    /// <summary>
    /// Interaktionslogik für RobotControl.xaml
    /// </summary>
    public partial class RobotControl : UserControl
    {
        public RobotControl()
        {
            InitializeComponent();
        }

        private void BtnStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Robot robot)
            {
                if (robot.IsSearching) robot.CancelSearch();
                else robot.BeginSearch();
            }
        }
    }
}
