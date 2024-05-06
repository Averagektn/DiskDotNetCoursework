using Disk.Entity;
using Disk.Extension;
using Disk.Repository.Implemetation;
using Disk.View;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddPatientViewModel : PopupViewModel
    {
        public ICommand AddPatientCommand => new Command(AddPatient);
        public ICommand AddRegionCommand => new Command(AddRegion);
        public ICommand AddDistrictCommand => new Command(AddDistrict);

        public District SelectedDistrict { get; set; } = new();
        public Region SelectedRegion { get; set; } = new();

        public ObservableCollection<Region> Regions { get; set; }
        public ObservableCollection<District> Districts { get; set; }

        private readonly AddressRepository _addressRepository = new();
        private readonly PatientRepository _patientRepository = new();
        private List<District> _districts;
        private List<Region> _regions;

        // Card
        private string _cardNumber = string.Empty;
        public string CardNumber { get => _cardNumber.Trim(); set => SetProperty(ref _cardNumber, value); }

        // Patient
        private string _name = string.Empty;
        private string _surname = string.Empty;
        private string _patronymic = string.Empty;
        private string _phoneMobile = string.Empty;
        private string _phoneHome = string.Empty;
        public string Name { get => _name.Trim().ToLower(); set => SetProperty(ref _name, value); }
        public string Surname { get => _surname.Trim().ToLower(); set => SetProperty(ref _surname, value); }
        public string Patronymic { get => _patronymic.Trim().ToLower(); set => SetProperty(ref _patronymic, value); }
        public string PhoneMobile { get => _phoneMobile.Trim().ToLower(); set => SetProperty(ref _phoneMobile, value); }
        public string PhoneHome { get => _phoneHome.Trim().ToLower(); set => SetProperty(ref _phoneHome, value); }
        public DateOnly Birthday { get; set; } = new DateOnly(2004, 5, 5);

        // Address
        private string _house = string.Empty;
        private string _apartment = string.Empty;
        private string _corpus = string.Empty;
        private string _street = string.Empty;
        public string House { get => _house.Trim().ToLower(); set => SetProperty(ref _house, value); }
        public string Apartment { get => _apartment.Trim().ToLower(); set => SetProperty(ref _apartment, value); }
        public string Corpus { get => _corpus.Trim().ToLower(); set => SetProperty(ref _corpus, value); }
        public string Street { get => _street.Trim().ToLower(); set => SetProperty(ref _street, value); }

        public AddPatientViewModel()
        {
            _districts = _addressRepository.GetDistrictsAsync().Result;
            Districts = new(_districts);

            _regions = _addressRepository.GetRegionsAsync().Result;
            Regions = new(_regions);
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

        public async void AddPatient(object? parameter)
        {
            var address = await AddAddressAsync();
            if (address is null)
            {
                return;
            }

            var patient = new Patient()
            {
                Address = address.Id,
                DateOfBirth = Birthday.ToString(),
                Patronymic = Patronymic,
                PhoneMobile = PhoneMobile,
                PhoneHome = PhoneHome
            };

            if (Name.IsEmpty())
            {
                await ShowPopup("Не введено имя");
                return;
            }
            patient.Name = Name;

            if (Surname.IsEmpty())
            {
                await ShowPopup("Не введена фамилия");
                return;
            }
            patient.Surname = Surname;

            if (CardNumber.IsEmpty())
            {
                await ShowPopup("Не указан номер карты");
                return;
            }

            try
            {
                await _patientRepository.AddPatientAsync(patient);
                await _patientRepository.AddCardAsync(new() { Number = CardNumber, Patient = patient.Id });
                await ShowPopup("Пациент успешно добавлен");
            }
            catch
            {
                await ShowPopup("Ошибка добавления пациента");
            }
        }

        private async Task<Address?> AddAddressAsync()
        {
            if (House.IsEmpty())
            {
                await ShowPopup("Дом является обязательным полем");
                return null;
            }

            if (Apartment.IsEmpty())
            {
                await ShowPopup("Квартира является обязательным полем");
                return null;
            }

            if (Street.IsEmpty())
            {
                await ShowPopup("Улица является обязательным полем");
                return null;
            }

            if (SelectedDistrict.Id == 0)
            {
                await ShowPopup("Выберите район");
                return null;
            }

            var address = new Address();
            try
            {
                address.Apartment = long.Parse(Apartment);
                address.Corpus = Corpus.IsEmpty() ? 1 : long.Parse(Corpus);
                address.House = long.Parse(House);
                address.Street = Street;
                address.District = SelectedDistrict.Id;

                address = await _addressRepository.AddAddressIfNotExistsAsync(address);
            }
            catch (Exception)
            {
                await ShowPopup("Указано некорректное значение для адреса");
                address = null;
            }

            return address;
        }

        public async void AddRegion(object? parameter)
        {
            new AddRegionWindow().ShowDialog();

            Regions.Clear();
            _regions = await _addressRepository.GetRegionsAsync();
            foreach (var region in _regions)
            {
                Regions.Add(region);
            }
        }

        public async void AddDistrict(object? parameter)
        {
            new AddDistrictWindow().ShowDialog();

            Districts.Clear();
            _districts = await _addressRepository.GetDistrictsAsync();
            foreach (var district in _districts)
            {
                Districts.Add(district);
            }
        }
    }
}