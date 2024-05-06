using Disk.AppSession;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddDiagnosisViewModel : PopupViewModel
    {
        private readonly PatientRepository _patientRepository = new();
        public ICommand AddDiagnosisCommand => new Command(AddDiagnosis);
        public string DiagnosisName { get; set; } = string.Empty;

        public async void AddDiagnosis(object? parameter)
        {
            DiagnosisName = DiagnosisName.Trim().ToLower();
            if (DiagnosisName.Length == 0)
            {
                await ShowPopup("Не задано наименование диагноза");
                return;
            }

            try
            {
                await _patientRepository.AddDiagnosisAsync(new() { DiagnosisNavigation = new() { Name = DiagnosisName }, Card = CurrentSession.Card.Id, DiagnosisStart = DateTime.Now.ToShortDateString() });
                await ShowPopup("Диагноз успешно добавлен");
            }
            catch (Exception)
            {
                await ShowPopup("Такой диагноз уже существует");
            }
        }
    }
}