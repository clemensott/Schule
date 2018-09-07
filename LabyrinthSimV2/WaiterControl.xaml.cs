using System.Windows;
using System.Windows.Controls;

namespace LabyrinthSim
{
    /// <summary>
    /// Interaktionslogik für WaiterControl.xaml
    /// </summary>
    public partial class WaiterControl : UserControl
    {
        public WaiterControl()
        {
            InitializeComponent();
        }

        private void BtnResume_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as Waiter)?.PulseAll();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Waiter waiter) waiter.Breakpoints.Add(new UIntText(waiter.Count));
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Waiter waiter && lbxBreakpoints.SelectedIndex >= 0)
            {
                waiter.Breakpoints.RemoveAt(lbxBreakpoints.SelectedIndex);
            }
        }

        private void LbxBreakpoints_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxBreakpoints.Items.Count > 0 && lbxBreakpoints.SelectedItem == null)
            {
                lbxBreakpoints.SelectedItem = lbxBreakpoints.Items[0];
            }
        }

        private void CbxUse_Checked(object sender, RoutedEventArgs e)
        {
            if (DataContext == null) DataContext = new Waiter();
        }

        private void CbxUse_Unchecked(object sender, RoutedEventArgs e)
        {
            DataContext = null;
        }
    }
}
