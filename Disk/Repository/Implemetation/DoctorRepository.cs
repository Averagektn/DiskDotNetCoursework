using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Exceptions;
using Disk.Repository.Interface;

namespace Disk.Repository.Implemetation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DiskContext _context = new();

        public async Task<long> PerformRegistrationAsync(Doctor doctor)
        {
            var a = _context
                .Doctors
                .Where(d => d.Name == doctor.Name && d.Surname == doctor.Surname && d.Patronymic == d.Patronymic 
                    && d.Password == doctor.Password)
                .FirstOrDefault();
            if (a is not null)
            {
                throw new DoctorDuplicationException();
            }

            var res = await _context.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return res.Entity.Id;
        }

        public async Task<long> PerformAuthorizationAsync(Doctor doctor)
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
