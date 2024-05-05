using Disk.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Disk.View
{
    public partial class AddPatientWindow : Window
    {
        private readonly AddPatientViewModel _viewModel = new();
        public AddPatientWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void RegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.OnRegionSelected();
        }
    }
}
