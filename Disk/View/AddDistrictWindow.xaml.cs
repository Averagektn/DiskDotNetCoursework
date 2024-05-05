using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddDistrictWindow : Window
    {
        public readonly AddDistrictViewModel _viewModel = new();

        public AddDistrictWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;   
        }
    }
}
