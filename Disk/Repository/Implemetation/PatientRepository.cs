using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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
            await _context.Contraindications.AddAsync(contraindication);
            await _context.SaveChangesAsync();
        }

        public async Task AddXrayAsync(Xray xray)
        {
            await _context.Xrays.AddAsync(xray);
            await _context.SaveChangesAsync();
        }

        public async Task AddDiagnosisAsync(M2mCardDiagnosis diagnosis)
        {
            var found = await _context.Diagnoses.Where(d => d.Name == diagnosis.DiagnosisNavigation.Name).FirstOrDefaultAsync();
            if (found is null)
            {
                var dia = new Diagnosis() { Name = diagnosis.DiagnosisNavigation.Name };
                await _context.Diagnoses.AddAsync(dia);
                await _context.SaveChangesAsync();
                await _context.M2mCardDiagnoses.AddAsync(new M2mCardDiagnosis() { Card = diagnosis.Card, Diagnosis = dia.Id, DiagnosisStart = diagnosis.DiagnosisStart });
                await _context.SaveChangesAsync();
                return;
            }

            await _context.M2mCardDiagnoses.AddAsync(new M2mCardDiagnosis() { Card = diagnosis.Card, Diagnosis = found.Id, DiagnosisStart = diagnosis.DiagnosisStart });
            await _context.SaveChangesAsync();
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
            return await _context.M2mCardDiagnoses.Where(m2m => m2m.Card == cardId).Include(m => m.DiagnosisNavigation).ToListAsync();
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
