using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

namespace Disk.Repository
{
    public class StaticticsRepository(DiskContext context) : IStatisticsRepository
    {
        public async Task<int> StartSessionAsync(Session session)
        {
            throw new NotImplementedException();
        }

        public async Task AddStatisticsAsync(int sessionId, SessionResult sessionResult, PathInTarget pathInTarget, 
            PathToTarget pathToTarget)
        {
            throw new NotImplementedException();
        }
    }
}
