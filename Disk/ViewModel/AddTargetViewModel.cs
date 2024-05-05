using Disk.AppSession;
using Disk.Repository.Implemetation;
using Disk.View;
using Disk.ViewModel.Common;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Disk.ViewModel
{
    public class AddTargetViewModel : PopupViewModel
    {
        private readonly DoctorRepository _doctorRepository = new();
        public ICommand AddTargetCommand => new Command(AddTarget);
        public ICommand OpenFileManagerCommand => new Command(OpenFileManager);
        private string _filePath = string.Empty;
        public string FilePath { get => _filePath; set => SetProperty(ref _filePath, value); }
        private string _fileName = string.Empty;
        public string FileName { get => _fileName; set => SetProperty(ref _fileName, value); }

        public void OpenFileManager(object? parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Картинки (*.png)|*.png"
            };
            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                FilePath = openFileDialog.FileName;
                FileName = Path.GetFileName(openFileDialog.FileName);
            }
        }

        public async void AddTarget(object? parameter)
        {
            FilePath = FilePath.Trim();
            if (FilePath.Length == 0)
            {
                await ShowPopup("Картинка не задана");
                return;
            }

            try
            {
                await _doctorRepository.AddTargetFileAsync(new() { Filepath = FilePath, AddedBy = CurrentSession.Doctor.Id });
                await ShowPopup("Картинка успешно добавлена");
            }
            catch (Exception)
            {
                await ShowPopup("Такая картинка уже существует");
            }

            Application.Current.Windows.OfType<AddTargetWindow>().First().Close();
        }
    }
}