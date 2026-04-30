using TaskManager.Domain.Enums;

namespace TaskManager.Application.DTOs
{
    public class TaskResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatusEnum Status { get; set; }
        public DateTime DueDate { get; set; }
    }
}