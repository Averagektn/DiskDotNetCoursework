using Disk.AppSession;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddContraindicationViewModel : PopupViewModel
    {
        private readonly PatientRepository _patientRepository = new();
        public ICommand AddContraindicationCommand => new Command(AddRegion);
        public string ContraindicationName { get; set; } = string.Empty;

        public async void AddRegion(object? parameter)
        {
            ContraindicationName = ContraindicationName.Trim().ToLower();
            if (ContraindicationName.Length == 0)
            {
                await ShowPopup("Не задано наименование противопоказания");
                return;
            }

            try
            {
                await _patientRepository.AddContraindicationAsync(new() { Name = ContraindicationName, Card = CurrentSession.Card.Id });
                await ShowPopup("Противопоказание успешно добавлено");
            }
            catch (Exception)
            {
                await ShowPopup("Такое противопоказание уже существует");
            }
        }
    }
}