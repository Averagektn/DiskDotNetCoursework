using Disk.AppSession;
using Disk.View;
using Disk.ViewModel.Common;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Settings = Disk.Properties.Config.Config;

namespace Disk.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        private string _doctorCredentials;
        public string DoctorCredentials { get => _doctorCredentials; set => SetProperty(ref _doctorCredentials, value); }

        public ICommand ChangeLanguage => new Command(ChangeLanguageClick);
        public ICommand ToPatientsClick => new Command(OnPatientsClick);
        public ICommand MapConstructorClick => new Command(OnMapContructorClick);
        public ICommand SettingsClick => new Command(OnSettingsClick);
        public ICommand StartClick => new Command(OnStartClick);
        public ICommand CalibrationClick => new Command(OnCalibrationClick);

        private static Settings Settings => Settings.Default;

        public MenuViewModel()
        {
            _doctorCredentials = CurrentSession.Doctor.ToString();

            if (!Directory.Exists(Settings.MAIN_DIR_PATH))
            {
                Directory.CreateDirectory(Settings.MAIN_DIR_PATH);
            }

            if (!Directory.Exists(Settings.MAPS_DIR_PATH))
            {
                Directory.CreateDirectory(Settings.MAPS_DIR_PATH);
            }
        }

        private void ChangeLanguageClick(object? parameter)
        {
            if (parameter is not null)
            {
                var selectedLanguage = parameter.ToString();

                Settings.LANGUAGE = selectedLanguage;
                Settings.Save();

                RestartApplication();
            }
        }

        private static void RestartApplication()
        {
            var appPath = Environment.ProcessPath;

            if (appPath is not null)
            {
                System.Diagnostics.Process.Start(appPath);
            }

            Application.Current.Shutdown();
        }

        private void OnPatientsClick(object? parameter)
        {
            Application.Current.Windows.OfType<MenuWindow>().First().Hide();
            new PatientsWindow().ShowDialog();
            Application.Current.Windows.OfType<MenuWindow>().First().Show();
        }

        private void OnMapContructorClick(object? parameter)
        {
            Application.Current.Windows.OfType<MenuWindow>().First().Hide();
            new MapCreator().ShowDialog();
            Application.Current.Windows.OfType<MenuWindow>().First().Show();
        }

        private void OnStartClick(object? parameter)
        {
            Application.Current.Windows.OfType<MenuWindow>().First().Hide();
            new UserDataForm().ShowDialog();
            Application.Current.Windows.OfType<MenuWindow>().First().Show();
        }

        private void OnSettingsClick(object? parameter)
        {
            Application.Current.Windows.OfType<MenuWindow>().First().Hide();
            new SettingsWindow().ShowDialog();
            Application.Current.Windows.OfType<MenuWindow>().First().Show();
        }

        private void OnCalibrationClick(object? parameter)
        {
            Application.Current.Windows.OfType<MenuWindow>().First().Hide();
            new CalibrationWindow().ShowDialog();
            Application.Current.Windows.OfType<MenuWindow>().First().Show();
        }
    }
}