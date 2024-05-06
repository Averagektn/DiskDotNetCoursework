using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddOperaionWindow : Window
    {
        private readonly AddOperaionViewModel _viewModel = new();

        public AddOperaionWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
