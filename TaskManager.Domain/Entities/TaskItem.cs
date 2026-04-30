using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public TaskStatusEnum Status { get; private set; }
        public DateTime DueDate { get; private set; }

        public TaskItem(string title, string description, DateTime dueDate)
        {
            Validate(title, dueDate);

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = TaskStatusEnum.Pending;
        }

        public void Update(string title, string description, DateTime dueDate)
        {
            Validate(title, dueDate);

            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        public void Start()
        {
            if (Status != TaskStatusEnum.Pending)
                throw new InvalidOperationException("Only pending tasks can be started.");

            Status = TaskStatusEnum.InProgress;
        }

        public void Complete()
        {
            if (Status != TaskStatusEnum.InProgress)
                throw new InvalidOperationException("Only tasks in progress can be completed.");

            Status = TaskStatusEnum.Done;
        }

        private static void Validate(string title, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (dueDate.Date < DateTime.UtcNow.Date)
                throw new ArgumentException("Due date cannot be in the past.");
        }
    }
}