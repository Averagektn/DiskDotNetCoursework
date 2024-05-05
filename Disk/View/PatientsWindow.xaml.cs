using Disk.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Disk.View
{
    public partial class PatientsWindow : Window
    {
        private readonly PatientsViewModel _viewModel = new();

        public PatientsWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            _viewModel.SearchText = textBox.Text;
            _viewModel.Find();
        }
    }
}
