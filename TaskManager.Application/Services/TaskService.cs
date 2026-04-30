using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services
{
    public class TaskService(ITaskRepository repository, IUnitOfWork unitOfWork)
    {
        private readonly ITaskRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Guid> CreateAsync(CreateTaskRequestDto request)
        {
            var task = new TaskItem(request.Title, request.Description, request.DueDate);

            await _repository.AddAsync(task);

            await _unitOfWork.CommitAsync();

            return task.Id;
        }

        public async Task<IEnumerable<TaskResponseDto>> GetAllAsync(TaskStatusEnum? status, DateTime? dueDate)
        {
            var tasks = await _repository.GetAllAsync(status, dueDate);

            return tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                DueDate = t.DueDate
            });
        }

        public async Task UpdateAsync(Guid id, CreateTaskRequestDto request)
        {
            var task = await _repository.GetByIdAsync(id);

            task.Update(request.Title, request.Description, request.DueDate);

            _repository.Update(task);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);

            _repository.Remove(task);

            await _unitOfWork.CommitAsync();
        }
    }
}
