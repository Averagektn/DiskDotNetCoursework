using Disk.Entity;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddRegionViewModel : PopupViewModel
    {
        private readonly AddressRepository _addressRepository = new();
        public ICommand AddRegionCommand => new Command(AddRegion);
        public Region Region { get; set; } = new();

        public async void AddRegion(object? parameter)
        {
            try
            {
                await _addressRepository.AddRegion(Region);
                await ShowPopup("Регион успешно добавлен");
                Region.Id = default;
            }
            catch (Exception)
            {
                await ShowPopup("Такой регион уже существует");
            }
        }
    }
}