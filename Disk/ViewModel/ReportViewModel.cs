using Disk.AppSession;
using Disk.Entity;
using Disk.Repository.Implemetation;
using Disk.ViewModel.Common;
using System.Collections.ObjectModel;

namespace Disk.ViewModel
{
    public class ReportViewModel : BaseViewModel
    {
        public Patient Patient { get; set; } = CurrentSession.Patient;
        public Doctor Doctor { get; set; } = CurrentSession.Doctor;

        public ObservableCollection<Session> Sessions { get; set; }
        public ObservableCollection<PathToTarget> PathToTargets { get; set; } = [];
        public Session SelectedSession { get; set; } = new();

        private readonly StatisticsRepository _statisticsRepository = new();

        public ReportViewModel()
        {
            Sessions = new(_statisticsRepository.GetSessionsAsync(CurrentSession.Appointment.Id).Result);
        }

        public void OnSessionSelected()
        {
            if (SelectedSession is not null && SelectedSession.Id != default)
            {
                PathToTargets.Clear();
                var paths = SelectedSession.PathToTargets;
                foreach (var path in paths)
                {
                    PathToTargets.Add(path);
                }
            }
        }
    }
}