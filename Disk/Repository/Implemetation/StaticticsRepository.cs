using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

namespace Disk.Repository.Implemetation
{
    public class StaticticsRepository : IStatisticsRepository
    {
        private readonly DiskContext _context = new();

        public async Task StartSessionAsync(Session session)
        {
            await _context.AddAsync(session);
            await _context.SaveChangesAsync();
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

        public async Task<List<Report>> GetReportAsync(long appointmentId)
        {
            throw new NotImplementedException();
        }
    }
}
