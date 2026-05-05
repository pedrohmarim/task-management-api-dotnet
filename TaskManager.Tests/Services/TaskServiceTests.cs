using FluentAssertions;
using Moq;
using TaskManager.Application.DTOs;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _repositoryMock = new Mock<ITaskRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _service = new TaskService(_repositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Task_When_Valid()
        {
            // Arrange
            var request = new CreateTaskRequestDto
            {
                Title = "Task",
                Description = "Desc",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            result.Should().NotBe(Guid.Empty);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_ValidationException_When_Title_Is_Empty()
        {
            // Arrange
            var request = new CreateTaskRequestDto
            {
                Title = "",
                Description = "Desc",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            // Act
            Func<Task> act = async () => await _service.CreateAsync(request);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_ValidationException_When_DueDate_Is_Past()
        {
            // Arrange
            var request = new CreateTaskRequestDto
            {
                Title = "Task",
                Description = "Desc",
                DueDate = DateTime.UtcNow.AddDays(-1)
            };

            // Act
            Func<Task> act = async () => await _service.CreateAsync(request);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_Tasks()
        {
            // Arrange
            var tasks = new List<TaskItem>
            {
                new("Task 1", "Desc", DateTime.UtcNow.AddDays(1)),
                new("Task 2", "Desc", DateTime.UtcNow.AddDays(2))
            };

            _repositoryMock.Setup(r => r.GetAllAsync(null, null)).ReturnsAsync(tasks);

            // Act
            var result = await _service.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Task_When_Found()
        {
            // Arrange
            var task = new TaskItem("Task", "Desc", DateTime.UtcNow.AddDays(1));

            _repositoryMock.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

            // Act
            var result = await _service.GetByIdAsync(task.Id);

            // Assert
            result.Id.Should().Be(task.Id);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Throw_NotFoundException_When_Not_Found()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((TaskItem?)null);

            // Act
            Func<Task> act = async () => await _service.GetByIdAsync(Guid.NewGuid());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Task_When_Valid()
        {
            // Arrange
            var task = new TaskItem("Old", "Desc", DateTime.UtcNow.AddDays(1));

            _repositoryMock.Setup(r => r.GetByIdAsync(task.Id)).ReturnsAsync(task);

            var request = new UpdateTaskRequestDto
            {
                Title = "Updated",
                Description = "Updated Desc",
                DueDate = DateTime.UtcNow.AddDays(2)
            };

            // Act
            await _service.UpdateAsync(task.Id, request);

            // Assert
            _repositoryMock.Verify(r => r.Update(task), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_NotFoundException_When_Task_Not_Found()
        {
            // Arrange
            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((TaskItem?)null);

            var request = new UpdateTaskRequestDto
            {
                Title = "Test",
                Description = "Test",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            // Act
            Func<Task> act = async () => await _service.UpdateAsync(Guid.NewGuid(), request);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_Should_Remove_Task_When_Found()
        {
            // Arrange
            var task = new TaskItem("Task", "Desc", DateTime.UtcNow.AddDays(1));

            _repositoryMock
                .Setup(r => r.GetByIdAsync(task.Id))
                .ReturnsAsync(task);

            // Act
            await _service.DeleteAsync(task.Id);

            // Assert
            _repositoryMock.Verify(r => r.Remove(task), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Throw_NotFoundException_When_Task_Not_Found()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((TaskItem?)null);

            // Act
            Func<Task> act = async () => await _service.DeleteAsync(Guid.NewGuid());

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}