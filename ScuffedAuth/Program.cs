using Identity.DAL.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScuffedAuth.Areas.Identity;
using ScuffedAuth.DAL;

namespace ScuffedAuth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider.MigrateScuffedAuth();
                using (var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>())
                {
                    context.Database.Migrate();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
