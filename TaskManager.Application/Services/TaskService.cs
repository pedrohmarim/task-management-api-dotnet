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

        // Em cenários mais complexos, poderia ser adotado um UnitOfWork específico por módulo,
        // onde o service dependeria de uma única abstração (ex: TaskUnitOfWork → TaskRepository).
        // Neste projeto, foi utilizado IUnitOfWork genérico + repositories injetados
        // para manter simplicidade e evitar overengineering.

        public async Task<Guid> CreateAsync(CreateTaskRequestDto request)
        {
            var task = new TaskItem(request.Title, request.Description, request.DueDate);

            await _repository.AddAsync(task);

            await _unitOfWork.CommitAsync();

            return task.Id;
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
