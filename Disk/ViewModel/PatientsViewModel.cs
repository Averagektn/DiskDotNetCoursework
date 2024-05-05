using Disk.Entity;
using Disk.Repository.Implemetation;
using Disk.View;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class PatientsViewModel : BaseViewModel
    {
        public Patient SelectedPatient { get; set; } = new();

        public ICommand AddPatient => new Command(ToAddPatient);
        public ICommand Find => new Command(FindCommand);
        private string _searchText = string.Empty;
        public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }

        public List<Patient> PatientsCollection = [];
        public ObservableCollection<Patient> Patients { get; set; }
        private readonly PatientRepository _patientRepository = new();

        public PatientsViewModel()
        {
            PatientsCollection = _patientRepository.GetPatientsAsync().Result;
            Patients = new(PatientsCollection);
        }

        public void ToAddPatient(object? parameter)
        {
            new AddPatientWindow().ShowDialog();
            //PatientsCollection = await _patientRepository.GetPatientsAsync();
            //Patients = new(PatientsCollection);
        }

        public void FindCommand(object? parameter)
        {
            Patients = new(PatientsCollection.Where(p => 
                $"{p.Surname} {p.Name} {p.Patronymic}".Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}