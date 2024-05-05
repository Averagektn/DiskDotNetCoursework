using Disk.AppSession;
using Disk.Entity;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;

namespace Disk.ViewModel
{
    public class PatientInfoViewModel : PopupViewModel
    {
        public ObservableCollection<(Appointment Appointment, string Date)> AppointmentsWithDate { get; set; } 
        public ObservableCollection<Appointment> Appointments { get; set; } 
        public ObservableCollection<Xray> Xrays { get; set; }
        public ObservableCollection<Contraindication> Contraindications { get; set; } 
        public ObservableCollection<Diagnosis> Diagnoses { get; set; } 
        public ObservableCollection<Operation> Operations { get; set; } 
        public ObservableCollection<Procedure> Procedures { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        public Appointment SelectedAppointment { get; set; } = new();
        public Card Card { get; set; }
        public Patient Patient = CurrentSession.Patient;

        public string AddressInCountry { get => _addressInCountry; set => SetProperty(ref _addressInCountry, value); }
        public string AddressInRegion { get => _addressInRegion; set => SetProperty(ref _addressInRegion, value); }

        private string _addressInCountry = string.Empty;
        private string _addressInRegion = string.Empty;

        private readonly PatientRepository _patientRepository = new();
        private readonly AddressRepository _addressRepository = new();

        public PatientInfoViewModel()
        {
            var address = _addressRepository.GetAddressByPatientIdAsync(Patient.Id).Result;
            _addressInCountry = $"{address.DistrictNavigation.Name}, {address.DistrictNavigation.RegionNavigation.Name}";
            _addressInRegion = $"{address.Street}, {address.House}-{address.Apartment}({address.Corpus})";

            Card = _patientRepository.GetCardByPatientIdAsync(CurrentSession.Patient.Id).Result;
        }
    }
}