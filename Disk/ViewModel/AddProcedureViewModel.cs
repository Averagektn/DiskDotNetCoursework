using Disk.AppSession;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddProcedureViewModel : PopupViewModel
    {
        private readonly DoctorPatientRepository _doctorPatientRepository = new();
        public ICommand AddProcedureCommand => new Command(AddProcedure);
        public string ProcedureName { get; set; } = string.Empty;
        public DateTime ProcedureDate { get; set; } = DateTime.Now;

        public async void AddProcedure(object? parameter)
        {
            ProcedureName = ProcedureName.Trim().ToLower();
            if (ProcedureName.Length == 0)
            {
                await ShowPopup("Не задано наименование процедуры");
                return;
            }

            try
            {
                await _doctorPatientRepository.AssignProcedureAsync(new() { Name = ProcedureName, AssignedBy = CurrentSession.Doctor.Id, AssignedTo = CurrentSession.Patient.Id, DateTime = ProcedureDate.ToShortDateString() });
                await ShowPopup("Процедура успешно добавлена");
            }
            catch (Exception)
            {
                await ShowPopup("Такая процедура уже существует");
            }
        }
    }
}