using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Disk.Repository.Implemetation
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DiskContext _context = new();

        public async Task StartSessionAsync(Session session)
        {
            await _context.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task AddPathToTargetAsync(PathToTarget pathToTarget)
        {
            await _context.PathToTargets.AddAsync(pathToTarget);
            await _context.SaveChangesAsync();
        }

        public async Task AddPathInTargetAsync(PathInTarget pathInTarget)
        {
            await _context.PathInTargets.AddAsync(pathInTarget);
            await _context.SaveChangesAsync();
        }

        public async Task AddSessionResultAsync(SessionResult sessionResult)
        {
            await _context.SessionResults.AddAsync(sessionResult);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Session>> GetSessionsAsync(long appointmentId)
        {
            return await _context.Sessions
                .Where(s => s.Appointment == appointmentId)
                .Include(s => s.PathInTargets)
                .Include(s => s.PathToTargets)
                .Include(s => s.SessionResult)
                .ToListAsync();
        }
    }
}
