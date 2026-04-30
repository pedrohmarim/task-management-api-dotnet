using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.UnitOfWork
{
    public class UnitOfWork(TaskDbContext context) : IUnitOfWork
    {
        private readonly TaskDbContext _context = context;

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}