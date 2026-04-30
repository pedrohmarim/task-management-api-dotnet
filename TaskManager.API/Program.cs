using Microsoft.AspNetCore.RateLimiting;

namespace TaskManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddUserSecrets<Program>(optional: true)
                .AddEnvironmentVariables();

            ConfigureWebHost(builder);

            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = 429;

                options.AddFixedWindowLimiter("api", opt =>
                {
                    opt.PermitLimit = 100;
                    opt.Window = TimeSpan.FromSeconds(10);
                    opt.QueueLimit = 0;
                });
            });

            var app = builder.Build();

            startup.Configure(app, app.Services);

            app.Run();
        }

        private static void ConfigureWebHost(WebApplicationBuilder builder)
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
        }
    }
}