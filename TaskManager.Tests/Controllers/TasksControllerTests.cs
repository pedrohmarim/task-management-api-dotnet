using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TaskManager.API;

namespace TaskManager.Tests.Controllers;

public class TasksControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Post_Task_Should_Return_201_Created()
    {
        // =========================
        // Arrange
        // =========================
        var request = new
        {
            title = "Integration Test",
            description = "Test Desc",
            dueDate = DateTime.UtcNow.AddDays(1)
        };

        // =========================
        // Act
        // =========================
        var response = await _client.PostAsJsonAsync("/tasks", request);

        // =========================
        // Assert
        // =========================
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Get_Tasks_Should_Return_200_OK()
    {
        // =========================
        // Arrange
        // =========================
        // Sem setup necessário para este teste :) 

        // =========================
        // Act
        // =========================
        var response = await _client.GetAsync("/tasks");

        // =========================
        // Assert
        // =========================
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_Task_Should_Return_400_When_Invalid()
    {
        // =========================
        // Arrange
        // =========================
        var request = new
        {
            title = "",
            description = "Desc",
            dueDate = DateTime.UtcNow.AddDays(1)
        };

        // =========================
        // Act
        // =========================
        var response = await _client.PostAsJsonAsync("/tasks", request);

        // =========================
        // Assert
        // =========================
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}