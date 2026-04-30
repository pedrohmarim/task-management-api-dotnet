using TaskManager.Application.Services;

namespace TaskManager.API.DependencyInjection
{
    public static class ConfigureBindingsApplication
    {
        public static void Register(IServiceCollection services)
        {
            // não precisa mockar (mocka só repository e unit of work)
            // não tem múltiplas implementações
            // não é uma abstração de domínio :)

            services.AddScoped<TaskService>();
        }
    }
}