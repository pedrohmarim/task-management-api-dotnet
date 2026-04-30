using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.UnitOfWork;

namespace TaskManager.API.DependencyInjection
{
    public static class ConfigureBindingsUnitOfWork
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}