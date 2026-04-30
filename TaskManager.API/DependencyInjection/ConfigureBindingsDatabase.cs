using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data;

namespace TaskManager.API.DependencyInjection
{
    public static class ConfigureBindingsDatabase
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TaskDbContext>(opt => opt.UseSqlite(connectionString));
        }
    }
}