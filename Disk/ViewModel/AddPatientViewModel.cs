using Disk.Entity;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddPatientViewModel : BaseViewModel
    {
        public ICommand AddPatientCommand => new Command(AddPatient);
        public ICommand AddRegionCommand => new Command(AddRegion);
        public ICommand AddDistrictCommand => new Command(AddDistrict);
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

        }

        public void AddPatient(object? parameter)
        {

        }

        public void AddRegion(object? parameter)
        {

        }

        public void AddDistrict(object? parameter)
        {

        }
    }
}