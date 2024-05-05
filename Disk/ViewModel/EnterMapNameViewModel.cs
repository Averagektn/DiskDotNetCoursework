using Disk.Repository.Implemetation;
using Disk.Repository.Interface;
using Disk.View;
using Disk.ViewModel.Common;
using System.Windows;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class EnterMapNameViewModel : PopupViewModel
    {
        public readonly DoctorRepository _doctorRepository = new();
        public string MapName { get; set; } = string.Empty;
        public ICommand AddMapCommand => new Command(AddMap);

        public async void AddMap(object? parameter)
        {
            MapName = MapName.Trim().ToLower();
            if (MapName.Length == 0)
            {
                await ShowPopup("Не задано имя карты");
                return;
            }

            if(await _doctorRepository.IsMapExists(MapName))
            {
                await ShowPopup("Карта с таким именем уже существует");
            }
            else
            {
                Application.Current.Windows.OfType<EnterMapNameWindow>().First().Close();
                new MapCreator() { MapName = MapName }.Show();
            }   
        }
    }
}