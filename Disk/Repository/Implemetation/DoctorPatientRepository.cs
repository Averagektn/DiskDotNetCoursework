using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Disk.Repository.Implemetation
{
    public class DoctorPatientRepository : IDoctorPatientRepository
    {
        private readonly DiskContext _context = new();

        public async Task<Appointment> AssignAppointmentAsync(Appointment appointment)
        {
            var res = await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task AssignProcedureAsync(Procedure procedure)
        {
            await _context.Procedures.AddAsync(procedure);
            await _context.SaveChangesAsync();
        }

        public async Task AssignOperationAsync(Operation operation)
        {
            await _context.Operations.AddAsync(operation);
            await _context.SaveChangesAsync();
        }

        public async Task AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Procedure>> GetProceduresAsync(long patientId)
        {
            return await _context.Procedures
                .Where(p => p.AssignedTo == patientId)
                .Include(p => p.AssignedByNavigation)
                .ToListAsync();
        }

        public async Task<List<Operation>> GetOperationsAsync(long cardId)
        {
            return await _context.Operations.Where(o => o.Card == cardId).Include(o => o.AsingnedByNavigation).ToListAsync();
        }

        public async Task<List<Note>> GetNotesAsync(long patientId)
        {
            return await _context.Notes.Where(n => n.Patient == patientId).Include(n => n.DoctorNavigation).ToListAsync();
        }

        public async Task<List<Map>> GetMapsAsync()
        {
            return await _context.Maps.ToListAsync();
        }

        public async Task<List<TargetFile>> GetTargetFilesAsync()
        {
            return await _context.TargetFiles.ToListAsync();
        }
    }
}
