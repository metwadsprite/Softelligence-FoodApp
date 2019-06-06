using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserInterface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();

            using (var services = webHost.Services.CreateScope())
            { 
                var identityInitializer = services.ServiceProvider.GetRequiredService<IdentityInitializer>();
                var configuration = services.ServiceProvider.GetRequiredService<IConfiguration>();
                identityInitializer.EnsureIdentityDb(configuration.GetConnectionString("DefaultConnection"));
                identityInitializer.InitializeDefaultRoles();
                identityInitializer.InitializeDefaultUsers();
            }
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
