using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

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

        public async Task AddNoteAsync(Note note)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Procedure>> GetProceduresAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Operation>> GetOperationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
