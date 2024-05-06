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
        public ICommand AddDistrictCommand => new Command(AddDistrict);
        public string DistrictName { get; set; } = string.Empty;
        public ObservableCollection<Region> Regions { get; set; }

        public AddDistrictViewModel()
        {
            var regions = _addressRepository.GetRegionsAsync().Result;
            Regions = new(regions);
        }

        public async void AddDistrict(object? parameter)
        {
            DistrictName = DistrictName.Trim().ToLower();
            if (DistrictName.Length == 0)
            {
                await ShowPopup("Не задано имя района");
                return;
            }
            if (SelectedRegion.Id == 0)
            {
                await ShowPopup("Не выбран регион");
                return;
            }

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