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

/*
        public async Task<Doctor> GetDoctorById(long id) 
            => await _context.Doctors.Where(d => d.Id == id).FirstOrDefaultAsync() ?? throw new DoctorNotFound();*/

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
