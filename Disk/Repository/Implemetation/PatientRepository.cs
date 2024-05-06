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
            await _context.Xrays.AddAsync(xray);
            await _context.SaveChangesAsync();
        }

        public async Task AddDiagnosisAsync(Diagnosis diagnosis)
        {
            throw new NotImplementedException();
        }

        public async Task CloseDiagnosisAsync(M2mCardDiagnosis diagnosis)
        {
            var found = await _context.M2mCardDiagnoses.Where(d => d.Card == diagnosis.Card && d.Diagnosis == diagnosis.Diagnosis).FirstAsync();
            found.DiagnosisFinish = diagnosis.DiagnosisFinish;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Contraindication>> GetContraindicationsAsync(long cardId)
        {
            return await _context.Contraindications.Where(c => c.Card == cardId).ToListAsync();
        }

        public async Task<List<Xray>> GetXraysAsync(long cardId)
        {
            return await _context.Xrays.Where(x => x.Card == cardId).ToListAsync();
        }

        public async Task<List<M2mCardDiagnosis>> GetDiagnosesAsync(long cardId)
        {
            return await _context.M2mCardDiagnoses.Where(m2m => m2m.Card == cardId).ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsAsync(long patientId)
        {
            return await _context.Appointments
                .Where(a => a.Patient == patientId)
                .Include(a => a.DoctorNavigation)
                .Include(a => a.Sessions)
                .ToListAsync();
        }
    }
}
