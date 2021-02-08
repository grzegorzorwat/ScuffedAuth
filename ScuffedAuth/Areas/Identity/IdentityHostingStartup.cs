using Identity.DAL.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScuffedAuth.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace ScuffedAuth.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityDbContext>(options =>
                    options.UseSqlServer(context.Configuration.GetConnectionString("IdentityDatabase")));
                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityDbContext>();
            });
        }
    }
}