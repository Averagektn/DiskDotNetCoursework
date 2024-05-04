using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

namespace Disk.Repository.Implemetation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DiskContext _context = new();

        public async Task<int> PerformRegistrationAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PerformAuthorizationAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public async Task AddMapAsync(Map map)
        {
            throw new NotImplementedException();
        }

        public async Task AddTargetFileAsync(TargetFile targetFile)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
