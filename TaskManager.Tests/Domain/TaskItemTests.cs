using FluentAssertions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Tests.Domain;

public class TaskItemTests
{
    [Fact]
    public void Constructor_Should_Create_Task_With_Valid_Data()
    {
        // =========================
        // Arrange
        // =========================
        var title = "Test Task";
        var description = "Desc";
        var dueDate = DateTime.UtcNow.AddDays(1);

        // =========================
        // Act
        // =========================
        var task = new TaskItem(title, description, dueDate);

        // =========================
        // Assert
        // =========================
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Status.Should().Be(TaskStatusEnum.Pending);
        task.DueDate.Should().Be(dueDate);
    }

    [Fact]
    public void Constructor_Should_Throw_When_Title_Is_Empty()
    {
        // =========================
        // Arrange
        // =========================
        var title = "";
        var description = "Desc";
        var dueDate = DateTime.UtcNow.AddDays(1);

        // =========================
        // Act
        // =========================
        TaskItem act() => new(title, description, dueDate);

        // =========================
        // Assert
        // =========================
        FluentActions
            .Invoking(act)
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Title cannot be empty.");
    }

    [Fact]
    public void Should_Change_Status_Correctly()
    {
        // =========================
        // Arrange
        // =========================
        var task = new TaskItem("Task", "Desc", DateTime.UtcNow.AddDays(1));

        // =========================
        // Act
        // =========================
        task.Start();
        var statusAfterStart = task.Status;

        task.Complete();
        var statusAfterComplete = task.Status;

        // =========================
        // Assert
        // =========================
        statusAfterStart.Should().Be(TaskStatusEnum.InProgress);
        statusAfterComplete.Should().Be(TaskStatusEnum.Done);
    }
}