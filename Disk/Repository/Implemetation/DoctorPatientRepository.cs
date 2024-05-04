﻿using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

namespace Disk.Repository
{
    public class DoctorPatientRepository : IDoctorPatientRepository
    {
        private DiskContext _context = new();

        public async Task<int> AssignAppointmentAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AssignProcedureAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddNoteAsync(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
