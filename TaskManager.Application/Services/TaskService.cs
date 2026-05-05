using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Application.Services
{
    public class TaskService(ITaskRepository repository, IUnitOfWork unitOfWork)
    {
        private readonly ITaskRepository _repository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        // Uso de UnitOfWork genérico + repositories para simplicidade.
        // Pode evoluir para UoW por módulo em cenários mais complexos.
        public async Task<Guid> CreateAsync(CreateTaskRequestDto task)
        {
            var taskDb = new TaskItem(task.Title, task.Description, task.DueDate);

            await _repository.AddAsync(taskDb);
            await _unitOfWork.CommitAsync();

            return taskDb.Id;
        }

        // Em cenários onde a API precisa atender listagens para UI (ex: tabelas com paginação),
        // seria possível evoluir esse método para um padrão mais completo de filtro dinâmico.
        //
        // Em outro projeto, implementei um BaseApplicationService com suporte a:
        // - Paginação
        // - Ordenação dinâmica
        // - Filtros por múltiplos campos (incluindo enums, datas e objetos)
        //
        // Exemplo:
        // FilterAsync(GenericTableFilterDto filter) → retorna TableView<T>
        // contendo lista + total de registros.
        //
        // Isso permite integração direta com componentes de UI (ex: tabelas do Ant Design),
        // mantendo a lógica de consulta centralizada e reutilizável.
        //
        // Neste projeto, mantive a implementação simples (filtros básicos por parâmetro) como o desafio pedia :)
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

        public async Task<TaskResponseDto> GetByIdAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Task with id '{id}' not found");

            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate
            };
        }

        public async Task UpdateAsync(Guid id, UpdateTaskRequestDto task)
        {
            var dbTask = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Task with id '{id}' not found");

            dbTask.Update(task.Title, task.Description, task.DueDate);

            _repository.Update(dbTask);

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id) ?? throw new NotFoundException($"Task with id '{id}' not found");

            _repository.Remove(task);

            await _unitOfWork.CommitAsync();
        }
    }
}