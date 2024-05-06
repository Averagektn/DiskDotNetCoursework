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
            _viewModel.OnContraidicationClick();
        }

        private void DiagnosisesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.OnDiagnosisClick();
        }

        private void XraysDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.OnXrayClick();
        }

        private void ProceduresDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.OnProcedureClick();
        }

        private void OperationsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.OnOperationClick();
        }

        private void NotesDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.OnNoteClick();
        }

        private void AppointmentsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _viewModel.OnAppointmentClick();
        }
    }
}
