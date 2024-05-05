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

        public async Task AddCardAsync(Card card)
        {
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
        }

        public async Task<Card> GetCardByPatientIdAsync(long patientId)
        {
            return await _context.Cards
                .Where(c => c.Patient == patientId)
                .Include(c => c.M2mCardDiagnoses)
                    .ThenInclude(m2m => m2m.DiagnosisNavigation)
                .Include(c => c.Contraindications)
                .Include(c => c.Xrays)
                .FirstAsync();
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

        public async Task<List<Contraindication>> GetContraindicationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task GetXraysAsync()
        {
            throw new NotImplementedException();
        }

        public async Task GetDiagnosesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
