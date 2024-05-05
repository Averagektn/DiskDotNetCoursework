using Disk.AppSession;
using Disk.Entity;
using Disk.Repository.Exceptions;
using Disk.Repository.Implemetation;
using Disk.View;
using Disk.ViewModel.Common;
using System.Security;
using System.Windows;

namespace Disk.ViewModel
{
    public class DoctorAuthenticationViewModel : PopupViewModel
    {
        public Doctor Doctor { get; set; } = new();
        public SecureString Password { get; set; } = new();
        private readonly DoctorRepository _doctorRepository = new();

        public async Task PerformRegistration(string password)
        {
            if (!(await Validate()))
            {
                return;
            }

            Doctor.Password = password;

            try
            {
                CurrentSession.Doctor = await _doctorRepository.PerformRegistrationAsync(Doctor);

                Application.Current.Windows.OfType<DoctorAuthenticationWindow>().First().Hide();
                new MenuWindow().ShowDialog();
                Application.Current.Windows.OfType<DoctorAuthenticationWindow>().First().Show();
            }
            catch (DoctorDuplicationException)
            {
                await ShowPopup("Такой врач уже зарегистрирован");
            }
        }

        public async Task PerformAuthorization(string password)
        {
            if (!(await Validate()))
            {
                return;
            }

            Doctor.Password = password;

            try
            {
                CurrentSession.Doctor = await _doctorRepository.PerformAuthorizationAsync(Doctor);

                Application.Current.Windows.OfType<DoctorAuthenticationWindow>().First().Hide();
                new MenuWindow().ShowDialog();
            }
            catch (DoctorNotFound)
            {
                await ShowPopup("Такого врача не существует");
            }
        }

        public async Task<bool> Validate()
        {
            Doctor.Name = Doctor.Name.Trim().ToLower();
            Doctor.Surname = Doctor.Surname.Trim().ToLower();
            Doctor.Patronymic = Doctor.Patronymic?.Trim().ToLower();

            if (Doctor.Name.Length == 0)
            {
                await ShowPopup("Имя обязательно");
                return false;
            }

            if (Doctor.Surname.Length == 0)
            {
                await ShowPopup("Фамилия обязательна");
                return false;
            }

            return true;
        }
    }
}