using Disk.AppSession;
using Disk.Entity;
using Disk.Repository.Implemetation;
using Disk.View;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class PatientsViewModel : BaseViewModel
    {
        public Patient SelectedPatient { get; set; } = new();

        public ICommand PatientClickCommand => new Command(ToPatientInfo);
        public ICommand AddPatientCommand => new Command(ToAddPatient);
        public string SearchText { get; set; } = string.Empty;
        public ObservableCollection<Patient> Patients { get; set; }

        private List<Patient> _patientsCollection;
        private readonly PatientRepository _patientRepository = new();

        public PatientsViewModel()
        {
            _patientsCollection = _patientRepository.GetPatientsAsync().Result;
            Patients = new(_patientsCollection);
        }

        public async void ToAddPatient(object? parameter)
        {
            new AddPatientWindow().ShowDialog();

            Patients.Clear();
            _patientsCollection = await _patientRepository.GetPatientsAsync();
            foreach (var patient in _patientsCollection)
            {
                Patients.Add(patient);
            }
        }

        public void ToPatientInfo(object? parameter)
        {
            if (SelectedPatient.Id != default)
            {
                CurrentSession.Patient = SelectedPatient;
                Application.Current.Windows.OfType<PatientsWindow>().First().Close();
                new PatientInfoWindow().ShowDialog();
            }
        }

        public void Find()
        {
            Patients.Clear();
            var patientns = _patientsCollection.Where(p => 
                $"{p.Surname} {p.Name} {p.Patronymic}".Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            foreach(var patient in patientns)
            {
                Patients.Add(patient);
            }
        }
    }
}