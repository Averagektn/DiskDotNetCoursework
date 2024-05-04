using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

namespace Disk.Repository.Implemetation
{
    public class StaticticsRepository : IStatisticsRepository
    {
        private readonly DiskContext _context = new();

        public async Task<int> StartSessionAsync(Session session)
        {
            throw new NotImplementedException();
        }

        public async Task AddStatisticsAsync(int sessionId, SessionResult sessionResult, PathInTarget pathInTarget,
            PathToTarget pathToTarget)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Session>> GetSessionsAsync(long appointmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<Report> GetReportAsync(long sessionId)
        {
            throw new NotImplementedException();
        }
    }
}
