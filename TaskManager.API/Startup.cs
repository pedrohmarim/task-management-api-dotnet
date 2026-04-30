using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskManager.API.DependencyInjection;
using TaskManager.Infrastructure.Data;

namespace TaskManager.API
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCore(services);
            ConfigureInfrastructure(services);
            ConfigureSwagger(services);
        }

        private static void ConfigureCore(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        private void ConfigureInfrastructure(IServiceCollection services)
        {
            ConfigureBindingsDependencyInjection.RegisterBindings(services, Configuration);
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Task Manager API",
                    Version = "v1",
                    Description = "API de gerenciamento de tarefas com DDD e Clean Architecture"
                });
            });
        }

        public void Configure(WebApplication app, IServiceProvider services)
        {
            ApplyMigrations(services);

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRateLimiter();

            app.MapControllers();
        }

        private static void ApplyMigrations(IServiceProvider services)
        {
            try
            {
                using var scope = services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<TaskDbContext>();

                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MIGRATION ERROR] {ex.Message}");
            }
        }
    }
}