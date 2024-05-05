using Disk.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace Disk.View
{
    public partial class PatientInfoWindow : Window
    {
        private readonly PatientInfoViewModel _viewModel = new();

        public PatientInfoWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void ContraindicationsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void DiagnosisesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void XraysDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void ProceduresDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void OperationsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void NotesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AppointmentsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
