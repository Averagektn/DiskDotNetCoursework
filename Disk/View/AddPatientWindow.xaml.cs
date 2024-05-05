using Disk.ViewModel;
using System.Windows;

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

        private void RegionComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _viewModel.OnRegionSelected();
        }
    }
}
