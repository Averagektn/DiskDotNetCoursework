using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class EnterMapNameWindow : Window
    {
        private readonly EnterMapNameViewModel _viewModel = new();

        public EnterMapNameWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
