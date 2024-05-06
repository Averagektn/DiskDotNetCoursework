using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddXrayWindow : Window
    {
        private readonly AddXrayViewModel _viewModel = new();

        public AddXrayWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
