using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Disk.Repository.Implemetation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DiskContext _context = new();

        public async Task<List<Patient>> GetPatientsAsync() =>
            await _context.Patients
                .Include(p => p.AddressNavigation)
                .ThenInclude(a => a.DistrictNavigation)
                .ThenInclude(d => d.RegionNavigation)
                .ToListAsync();

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            var res = await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return res.Entity;
        }

        public async Task AddCardAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePatientAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddAddressAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddContraindicationAsync(Contraindication contraindication)
        {
            throw new NotImplementedException();
        }

        public async Task AddXrayAsync(Xray xray)
        {
            throw new NotImplementedException();
        }

        public async Task AddDiagnosisAsync(Diagnosis diagnosis)
        {
            throw new NotImplementedException();
        }

        public async Task CloseDiagnosisAsync(Diagnosis diagnosis)
        {

        }
    }
}
