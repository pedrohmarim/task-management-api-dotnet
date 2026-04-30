using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.API.DependencyInjection
{
    public static class ConfigureBindingsRepository
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
        }
    }
}