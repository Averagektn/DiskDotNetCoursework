using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddProcedureWindow : Window
    {
        private readonly AddProcedureViewModel _viewModel = new();

        public AddProcedureWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
