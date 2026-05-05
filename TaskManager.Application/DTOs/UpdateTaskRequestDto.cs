namespace TaskManager.Application.DTOs
{
    public class UpdateTaskRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}