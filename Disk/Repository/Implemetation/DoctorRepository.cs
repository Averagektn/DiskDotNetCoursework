using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Exceptions;
using Disk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Disk.Repository.Implemetation
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DiskContext _context = new();

        public async Task<long> PerformRegistrationAsync(Doctor doctor)
        {
            var doc = await _context
                .Doctors
                .Where(d => d.Name == doctor.Name && d.Surname == doctor.Surname && d.Patronymic == d.Patronymic 
                    && d.Password == doctor.Password)
                .FirstOrDefaultAsync();
            if (doc is not null)
            {
                throw new DoctorDuplicationException();
            }

            var res = await _context.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return res.Entity.Id;
        }

        public async Task<long> PerformAuthorizationAsync(Doctor doctor)
        {
            var doc = await _context
                .Doctors
                .Where(d => d.Name == doctor.Name && d.Surname == doctor.Surname && d.Patronymic == d.Patronymic
                    && d.Password == doctor.Password)
                .FirstOrDefaultAsync() ?? throw new DoctorNotFound();

            return doc.Id;
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
