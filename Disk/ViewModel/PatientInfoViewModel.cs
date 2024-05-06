using Disk.AppSession;
using Disk.Entity;
using Disk.Extension;
using Disk.Repository.Implemetation;
using Disk.View;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class PatientInfoViewModel : PopupViewModel
    {
        public ObservableCollection<Appointment> Appointments { get; set; } 
        public ObservableCollection<Xray> Xrays { get; set; }
        public ObservableCollection<Contraindication> Contraindications { get; set; } 
        public ObservableCollection<M2mCardDiagnosis> Diagnoses { get; set; } 
        public ObservableCollection<Operation> Operations { get; set; } 
        public ObservableCollection<Procedure> Procedures { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        public ICommand StartAppointmentCommand => new Command(StartAppointment);

        public Appointment SelectedAppointment { get; set; } = new();
        public M2mCardDiagnosis SelectedDiagnosis { get; set; } = new();
        public Card Card { get; set; }
        public Patient Patient { get; set; } = CurrentSession.Patient;

        public string AddressInCountry { get => _addressInCountry; set => SetProperty(ref _addressInCountry, value); }
        public string AddressInRegion { get => _addressInRegion; set => SetProperty(ref _addressInRegion, value); }

        private string _addressInCountry = string.Empty;
        private string _addressInRegion = string.Empty;

        private readonly PatientRepository _patientRepository = new();
        private readonly AddressRepository _addressRepository = new();
        private readonly DoctorPatientRepository _doctorPatientRepository = new();

        public PatientInfoViewModel()
        {
            var address = _addressRepository.GetAddressByPatientIdAsync(Patient.Id).Result;
            _addressInCountry = $"{address.DistrictNavigation.Name.ToUpperFirstLetter()}, {address.DistrictNavigation.RegionNavigation.Name.ToUpperFirstLetter()}";
            _addressInRegion = $"{address.Street.ToUpperFirstLetter()}, {address.House}-{address.Apartment}({address.Corpus})";

            Card = _patientRepository.GetCardByPatientIdAsync(CurrentSession.Patient.Id).Result;
            CurrentSession.Card = Card;

            Appointments = new(_patientRepository.GetAppointmentsAsync(Patient.Id).Result);
            Xrays = new(_patientRepository.GetXraysAsync(Card.Id).Result);
            Contraindications = new(_patientRepository.GetContraindicationsAsync(Card.Id).Result);
            Diagnoses = new(_patientRepository.GetDiagnosesAsync(Card.Id).Result);
            Operations = new(_doctorPatientRepository.GetOperationsAsync(Card.Id).Result);
            Procedures = new(_doctorPatientRepository.GetProceduresAsync(Patient.Id).Result);
            Notes = new(_doctorPatientRepository.GetNotesAsync(Patient.Id).Result);
        }

        public void StartAppointment(object? parameter)
        {
            MessageBoxResult result = MessageBox.Show("Начать новый сеанс?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

            }
            else if (result == MessageBoxResult.No)
            {
                
            }
        }

        public void OnAppointmentClick()
        {
            if (SelectedAppointment.Id == default)
            {
                return;
            }
        }

        public async void OnXrayClick()
        {
            new AddXrayWindow().ShowDialog();

            Xrays.Clear();
            var xrays = await _patientRepository.GetXraysAsync(Card.Id);
            foreach (var xray in xrays)
            {
                Xrays.Add(xray);
            }
        }

        public async void OnContraidicationClick()
        {
            new AddContraindicationWindow().ShowDialog();

            Contraindications.Clear();
            var contraindications = await _patientRepository.GetContraindicationsAsync(Card.Id);
            foreach (var contraindication in contraindications)
            {
                Contraindications.Add(contraindication);
            }
        }

        public async void OnOperationClick()
        {
            new AddOperaionWindow().ShowDialog();

            Operations.Clear();
            var operations = await _doctorPatientRepository.GetOperationsAsync(Card.Id);
            foreach (var operation in operations)
            {
                Operations.Add(operation);
            }
        }

        public async void OnProcedureClick()
        {
            new AddProcedureWindow().ShowDialog();

            Procedures.Clear();
            var procedures = await _doctorPatientRepository.GetProceduresAsync(CurrentSession.Patient.Id);
            foreach (var procedure in procedures)
            {
                Procedures.Add(procedure);
            }
        }

        public async void OnNoteClick()
        {

        }

        public async void OnDiagnosisClick()
        {
            if (SelectedDiagnosis is null || SelectedDiagnosis.Diagnosis == default)
            {
                new AddDiagnosisWindow().ShowDialog();
            }
            else
            {
                if (SelectedDiagnosis.DiagnosisFinish is null || SelectedDiagnosis.DiagnosisFinish.IsEmpty())
                {
                    SelectedDiagnosis.DiagnosisFinish = DateTime.Now.ToShortDateString();
                    await _patientRepository.CloseDiagnosisAsync(SelectedDiagnosis);
                }
            }

            Diagnoses.Clear();
            var diagnoses = await _patientRepository.GetDiagnosesAsync(Card.Id);
            foreach (var diagnosis in diagnoses)
            {
                Diagnoses.Add(diagnosis);
            }
        }
    }
}