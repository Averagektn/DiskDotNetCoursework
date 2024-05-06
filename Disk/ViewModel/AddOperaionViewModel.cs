using Disk.AppSession;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddOperaionViewModel : PopupViewModel
    {
        private readonly DoctorPatientRepository _doctorPatientRepository = new();
        public ICommand AddOperationCommand => new Command(AddContraindication);
        public string OperationName { get; set; } = string.Empty;
        public DateTime OperationDate { get; set; } = DateTime.Now;

        public async void AddContraindication(object? parameter)
        {
            OperationName = OperationName.Trim().ToLower();
            if (OperationName.Length == 0)
            {
                await ShowPopup("Не задано наименование операции");
                return;
            }

            try
            {
                await _doctorPatientRepository.AssignOperationAsync(new() { Name = OperationName, AsingnedBy = CurrentSession.Doctor.Id, Card = CurrentSession.Card.Id, DateTime = OperationDate.ToShortDateString() });
                await ShowPopup("Операция успешно добавлена");
            }
            catch (Exception)
            {
                await ShowPopup("Такая операция уже существует");
            }
        }
    }
}