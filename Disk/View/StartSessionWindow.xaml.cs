using Disk.ViewModel;
using System.Windows;

namespace Disk
{
    public partial class StartSessionWindow : Window
    {
        private readonly StartSessionViewModel _viewModel = new();
        
        public StartSessionWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
