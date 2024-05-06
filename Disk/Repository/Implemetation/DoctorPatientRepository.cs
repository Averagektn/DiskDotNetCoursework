using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Disk.Repository.Implemetation
{
    public class DoctorPatientRepository : IDoctorPatientRepository
    {
        private readonly DiskContext _context = new();

        public async Task<int> AssignAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public async Task AssignProcedureAsync(Procedure procedure)
        {
            throw new NotImplementedException();
        }

        public async Task AssignOperationAsync(Operation operation)
        {
            await _context.Operations.AddAsync(operation);
            await _context.SaveChangesAsync();
        }

        public async Task AddNoteAsync(Note note)
        {
            throw new NotImplementedException();
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
    }
}
