using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddDiagnosisWindow : Window
    {
        private readonly AddDiagnosisViewModel _viewModel = new();

        public AddDiagnosisWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
