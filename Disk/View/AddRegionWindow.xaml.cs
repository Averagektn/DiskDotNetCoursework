using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddRegionWindow : Window
    {
        private readonly AddRegionViewModel _viewModel = new();

        public AddRegionWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
