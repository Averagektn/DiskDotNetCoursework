using Disk.Entity;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddDistrictViewModel : PopupViewModel
    {
        public Region SelectedRegion { get; set; } = new();
        private readonly AddressRepository _addressRepository = new();
        public ICommand AddDistrictCommand => new Command(AddRegion);
        public string DistrictName { get; set; } = string.Empty;
        public ObservableCollection<Region> Regions { get; set; }

        public AddDistrictViewModel()
        {
            var regions = _addressRepository.GetRegionsAsync().Result;
            Regions = new(regions);
        }

        public async void AddRegion(object? parameter)
        {
            try
            {
                await _addressRepository.AddDistrict(new() { Name = DistrictName, Region = SelectedRegion.Id });
                await ShowPopup("Район успешно добавлен");
            }
            catch (Exception)
            {
                await ShowPopup("Такой район уже существует");
            }
        }
    }
}