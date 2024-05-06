using Disk.AppSession;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddNoteViewModel : PopupViewModel
    {
        private readonly DoctorPatientRepository _doctorPatientRepository = new();
        public ICommand AddNoteCommand => new Command(AddNote);
        public string NoteText { get; set; } = string.Empty;

        public async void AddNote(object? parameter)
        {
            NoteText = NoteText.Trim().ToLower();
            if (NoteText.Length == 0)
            {
                await ShowPopup("Не задан текст заметки");
                return;
            }

            try
            {
                await _doctorPatientRepository.AddNoteAsync(new() { Text = NoteText, Doctor = CurrentSession.Doctor.Id, Patient = CurrentSession.Patient.Id });
                await ShowPopup("Заметка успешно добавлена");
            }
            catch (Exception)
            {
                await ShowPopup("Такая заметка уже существует");
            }
        }
    }
}