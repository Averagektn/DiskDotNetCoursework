using Disk.AppSession;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using Microsoft.Win32;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddXrayViewModel : PopupViewModel
    {
        private readonly PatientRepository _patientRepository = new();
        public ICommand AddXrayCommand => new Command(AddXray);
        public ICommand OpenFileManagerCommand => new Command(OpenFileManager);
        private string _filePath = string.Empty;
        public string FilePath { get => _filePath; set => SetProperty(ref _filePath, value); }

        public void OpenFileManager(object? parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Рентген (*)|*"
            };
            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                FilePath = openFileDialog.FileName;
            }
        }

        public async void AddXray(object? parameter)
        {
            FilePath = FilePath.Trim();
            if (FilePath.Length == 0)
            {
                await ShowPopup("Рентген не задан");
                return;
            }

            try
            {
                await _patientRepository.AddXrayAsync(new() { FilePath = FilePath, Date = DateTime.Now.ToShortDateString(), Description = "Рентген", Card = CurrentSession.Card.Id });
                await ShowPopup("Рентген успешно добавлен");
            }
            catch (Exception)
            {
                await ShowPopup("Такой рентген уже существует");
            }
        }
    }
}