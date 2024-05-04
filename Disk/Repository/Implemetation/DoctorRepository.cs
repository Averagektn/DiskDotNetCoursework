using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

namespace Disk.Repository
{
    public class DoctorRepository(DiskContext context) : IDoctorRepository
    {
        private DiskContext _context = new();

        public async Task<int> PerformRegistrationAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public async Task<int> PerformAuthorizationAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public async Task EditAccountAsync(Doctor doctor)
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

        public async Task AssignProcedure(Procedure procedure)
        {
            throw new NotImplementedException();
        }
    }
}
