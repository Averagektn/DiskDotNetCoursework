using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddTargetWindow : Window
    {
        private readonly AddTargetViewModel _viewModel = new();

        public AddTargetWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
