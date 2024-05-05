using Disk.AppSession;
using Disk.Entity;
using Disk.Repository.Exceptions;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Security;
using System.Windows;

namespace Disk.ViewModel
{
    public class DoctorRegistrationViewModel : BaseViewModel
    {
        public Doctor Doctor { get; set; } = new();
        public SecureString Password { get; set; } = new();
        private readonly DoctorRepository _doctorRepository = new();

        private string _popupMessage = string.Empty;
        public string PopupMessage { get => _popupMessage; set => SetProperty(ref _popupMessage, value); }

        private bool _isShowPopup;
        public bool IsShowPopup { get => _isShowPopup; set => SetProperty(ref _isShowPopup, value); }

        public async Task PerformRegistration(string password)
        {
            Doctor.Name = Doctor.Name.Trim().ToLower();
            Doctor.Surname = Doctor.Surname.Trim().ToLower();
            Doctor.Patronymic = Doctor.Patronymic?.Trim().ToLower();

            if (Doctor.Name.Length == 0)
            {
                await ShowPopup("Имя обязательно");
                return;
            }

            if (Doctor.Surname.Length == 0)
            {
                await ShowPopup("Фамилия обязательна");
                return;
            }

            Doctor.Password = password;

            try
            {
                CurrentSession.DoctorId = await _doctorRepository.PerformRegistrationAsync(Doctor);

                Application.Current.MainWindow.Hide();
                new MenuWindow().ShowDialog();
                Application.Current.MainWindow.Show();
            }
            catch (DoctorDuplicationException)
            {
                await ShowPopup("Такой врач уже зарегистрирован");
            }
        }

        public async Task ShowPopup(string message)
        {
            PopupMessage = message;
            IsShowPopup = true;
            await Task.Delay(TimeSpan.FromSeconds(3));
            IsShowPopup = false;
        }
    }
}