using Disk.AppSession;
using Disk.Data.Impl;
using Disk.Entity;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using Settings = Disk.Properties.Config.Config;

namespace Disk.ViewModel
{
    public class StartSessionViewModel : PopupViewModel
    {
        public Map SelectedMap { get; set; } = new();
        public TargetFile SelectedTargetFile { get; set; } = new();
        public Appointment Appointment { get; set; } = CurrentSession.Appointment;
        public Doctor Doctor { get; set; } = CurrentSession.Doctor;
        public Patient Patient { get; set; } = CurrentSession.Patient;

        public ObservableCollection<Map> Maps { get; set; }
        public ObservableCollection<TargetFile> TargetFiles { get; set; }

        public ICommand StartClick => new Command(OnStartClick);

        private static Settings Settings => Settings.Default;

        private readonly DoctorPatientRepository _doctorPatientRepository = new();
        private readonly StaticticsRepository _staticticsRepository = new();

        public StartSessionViewModel()
        {
            Maps = new(_doctorPatientRepository.GetMapsAsync().Result);
            TargetFiles = new(_doctorPatientRepository.GetTargetFilesAsync().Result);
        }

        private async void OnStartClick(object? obj)
        {
            if (SelectedTargetFile is null || SelectedTargetFile.Id == default)
            {
                await ShowPopup("Мишень не выбрана");
                return;
            }

            if (SelectedMap is null || SelectedMap.Id == default)
            {
                await ShowPopup("Карта не выбрана");
                return;
            }

            var session = new Session() { Appointment = CurrentSession.Appointment.Id, Date = DateTime.Now.ToString(),
                LogFilePath =
                $"{Settings.MAIN_DIR_PATH}{Path.DirectorySeparatorChar}" +
                $"{Patient.Surname} {Patient.Name}{Path.DirectorySeparatorChar}" +
                $"{DateTime.Now}", 
                Map = SelectedMap.Id };
            await _staticticsRepository.StartSessionAsync(session);
            CurrentSession.Session = session; 

            new PaintWindow()
            {
                DbTargetFilePath = SelectedTargetFile.Filepath,
                DbMapCenters = JsonConvert.DeserializeObject<List<Point2D<float>>>(SelectedMap.CoordinatesJson) ?? [],
                CurrPath =
                $"{Settings.MAIN_DIR_PATH}{Path.DirectorySeparatorChar}" +
                $"{Patient.Surname} {Patient.Name}{Path.DirectorySeparatorChar}" +
                $"{DateTime.Now:dd.MM.yyyy HH-mm-ss}",
            }
            .ShowDialog();
        }
    }
}