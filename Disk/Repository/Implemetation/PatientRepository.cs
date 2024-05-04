using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

namespace Disk.Repository.Implemetation
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DiskContext _context = new();

        public async Task<int> AddPatientAsync(Patient patient)
        {
            throw new NotImplementedException();
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
