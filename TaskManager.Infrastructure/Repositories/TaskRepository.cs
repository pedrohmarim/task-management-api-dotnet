using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository(TaskDbContext context) : ITaskRepository
    {
        private readonly TaskDbContext _context = context;

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync(TaskStatusEnum? status, DateTime? dueDate)
        {
            var query = _context.Tasks.AsQueryable();

            if (status.HasValue)
                query = query.Where(x => x.Status == status);

            if (dueDate.HasValue)
                query = query.Where(x => x.DueDate.Date == dueDate.Value.Date);

            return await query.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public void Remove(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
        }
    }
}