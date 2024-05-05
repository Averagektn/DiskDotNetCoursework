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

        public async Task<Doctor> PerformRegistrationAsync(Doctor doctor)
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

            return res.Entity;
        }

        public async Task<Doctor> PerformAuthorizationAsync(Doctor doctor) =>
            await _context.Doctors
                .Where(d => d.Name == doctor.Name && d.Surname == doctor.Surname && d.Patronymic == d.Patronymic
                    && d.Password == doctor.Password)
                .FirstOrDefaultAsync() ?? throw new DoctorNotFoundException();

        public async Task<bool> IsMapExists(string mapName)
        {
            return await _context.Maps.AnyAsync(m => m.Name == mapName);
        }

        public async Task AddMapAsync(Map map)
        {
            await _context.Maps.AddAsync(map);
            await _context.SaveChangesAsync();
        }

        public async Task AddTargetFileAsync(TargetFile targetFile)
        {
            await _context.AddAsync(targetFile);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
