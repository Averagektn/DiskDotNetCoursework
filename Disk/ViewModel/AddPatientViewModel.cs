using Disk.Entity;
using Disk.View;
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
        private readonly List<District> _districts = [];

        public AddPatientViewModel()
        {
            Regions.Add(new() { Name = "ABOBA" });
            Regions.Add(new() { Name = "SUS" });
        }

        public void OnRegionSelected()
        {
            Districts.Clear();
            var districts = _districts.Where(d => d.Region == SelectedRegion.Id);
            foreach (var district in districts)
            {
                Districts.Add(district);
            }
        }

        public void AddPatient(object? parameter)
        {

        }

        public void AddRegion(object? parameter)
        {
            new AddRegionWindow().ShowDialog();
        }

        public void AddDistrict(object? parameter)
        {
            new AddDistrictWindow().ShowDialog();
        }
    }
}