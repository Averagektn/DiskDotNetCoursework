using Disk.Entity;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddPatientViewModel : BaseViewModel
    {
        public ICommand AddPatientCommand => new Command(AddPatient);
        public Patient Patient { get; set; } = new();
        public Address Address { get; set; } = new();
        public District SelectedDistrict { get; set; } = new();
        public Region SelectedRegion { get; set; } = new();
        public ObservableCollection<Region> Regions { get; set; } = [];
        public ObservableCollection<District> Districts { get; set; } = [];

        public AddPatientViewModel()
        {
            Regions.Add(new() { Name = "ABOBA" });
            Regions.Add(new() { Name = "SUS" });
        }

        public void OnRegionSelected()
        {
            Console.WriteLine("A");
        }

        public void AddPatient(object? parameter)
        {

        }
    }
}