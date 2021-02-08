using Authentication.ClientCredentials;
using Authorization.TokenEndpoint;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using AuthorizationCode = Authorization.AuthorizationCode;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;

namespace ScuffedAuth.DAL
{
    public static class ServiceConfiguration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("scuffed-auth-in-memory");
                });
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<AuthorizationEndpoint.IAuthorizationCodesRepository, AuthorizationCodesRepository>();
            services.AddScoped<AuthorizationCode.IAuthorizationCodesRepository, AuthorizationCodesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void EnsureInMemoryDatabaseCreated(this IServiceProvider service)
        {
            using var context = service.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
        }
    }
}
