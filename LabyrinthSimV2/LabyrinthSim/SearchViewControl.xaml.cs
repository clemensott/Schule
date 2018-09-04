using System.Windows;
using System.Windows.Controls;

namespace LabyrinthSim
{
    /// <summary>
    /// Interaktionslogik für SearchViewControl.xaml
    /// </summary>
    public partial class SearchViewControl : UserControl
    {
        public SearchViewControl()
        {
            InitializeComponent();
        }

        private void BtnStartStop_Click(object sender, RoutedEventArgs e)
        {
            SearchView view = DataContext as SearchView;

            if (view.IsSearching) view.CancelSearch();
            else view.BeginSearch();
        }
    }
}
