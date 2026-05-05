using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.API;

namespace TaskManager.Tests.Controllers
{
    public class TasksControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Post_Task_Should_Return_201_And_Location()
        {
            // Arrange
            var request = new
            {
                title = "Integration Test",
                description = "Test Desc",
                dueDate = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/tasks", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // valida header Location
            response.Headers.Location.Should().NotBeNull();

            // valida retorno do id
            var id = await response.Content.ReadFromJsonAsync<Guid>();

            id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Get_Tasks_Should_Return_200_OK()
        {
            // Act
            var response = await _client.GetAsync("/tasks?dueDate=2024-12-31");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Tasks_With_Invalid_Date_Should_Return_400()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/tasks?dueDate=invalid-date");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Task_By_Id_Should_Return_200_When_Exists()
        {
            // Arrange
            var request = new
            {
                title = "Task for GetById",
                description = "Desc",
                dueDate = DateTime.UtcNow.AddDays(1)
            };

            var createResponse = await _client.PostAsJsonAsync("/tasks", request);
            var id = await createResponse.Content.ReadFromJsonAsync<Guid>();

            // Act
            var response = await _client.GetAsync($"/tasks/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Get_Task_By_Id_Should_Return_404_When_Not_Found()
        {
            // Act
            var response = await _client.GetAsync($"/tasks/{Guid.NewGuid()}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Post_Task_Should_Return_400_When_Invalid()
        {
            // Arrange
            var request = new
            {
                title = "", // inválido
                description = "Desc",
                dueDate = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/tasks", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_Task_Should_Return_204_When_Updated()
        {
            // Arrange - cria
            var request = new
            {
                title = "Task Update",
                description = "Desc",
                dueDate = DateTime.UtcNow.AddDays(1)
            };

            var createResponse = await _client.PostAsJsonAsync("/tasks", request);
            var id = await createResponse.Content.ReadFromJsonAsync<Guid>();

            var updateRequest = new
            {
                title = "Updated",
                description = "Updated Desc",
                dueDate = DateTime.UtcNow.AddDays(2)
            };

            // Act
            var response = await _client.PutAsJsonAsync($"/tasks/{id}", updateRequest);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Task_Should_Return_204_When_Deleted()
        {
            // Arrange - cria
            var request = new
            {
                title = "Task Delete",
                description = "Desc",
                dueDate = DateTime.UtcNow.AddDays(1)
            };

            var createResponse = await _client.PostAsJsonAsync("/tasks", request);
            var id = await createResponse.Content.ReadFromJsonAsync<Guid>();

            // Act
            var response = await _client.DeleteAsync($"/tasks/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_Task_Should_Return_404_When_Not_Found()
        {
            // Act
            var response = await _client.DeleteAsync($"/tasks/{Guid.NewGuid()}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}