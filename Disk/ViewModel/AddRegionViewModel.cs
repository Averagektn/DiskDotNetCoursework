﻿using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddRegionViewModel : PopupViewModel
    {
        private readonly AddressRepository _addressRepository = new();
        public ICommand AddRegionCommand => new Command(AddRegion);
        public string RegionName { get; set; } = string.Empty;

        public async void AddRegion(object? parameter)
        {
            RegionName = RegionName.Trim().ToLower();
            if (RegionName.Length == 0)
            {
                await ShowPopup("Не задано имя региона");
                return;
            }

            try
            {
                await _addressRepository.AddRegion(new() { Name = RegionName });
                await ShowPopup("Регион успешно добавлен");
            }
            catch (Exception)
            {
                await ShowPopup("Такой регион уже существует");
            }
        }
    }
}