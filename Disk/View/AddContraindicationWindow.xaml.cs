using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddContraindicationWindow : Window
    {
        private readonly AddContraindicationViewModel _viewModel = new();

        public AddContraindicationWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
