using TaskManager.Application.Services;

namespace TaskManager.API.DependencyInjection
{
    public static class ConfigureBindingsApplication
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<TaskService>();
        }
    }
}