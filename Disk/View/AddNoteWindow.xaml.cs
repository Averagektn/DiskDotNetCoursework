using Disk.ViewModel;
using System.Windows;

namespace Disk.View
{
    public partial class AddNoteWindow : Window
    {
        private readonly AddNoteViewModel _viewModel = new();

        public AddNoteWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
