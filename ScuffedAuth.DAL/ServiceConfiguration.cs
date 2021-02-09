using Authorization.TokenEndpoint;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScuffedAuth.DAL.Entities;
using ScuffedAuth.DAL.Mapping;
using System;
using AuthorizationCode = Authorization.AuthorizationCode;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;
using ClientCredentials = Authentication.ClientCredentials;

namespace ScuffedAuth.DAL
{
    public static class ServiceConfiguration
    {
        public static void AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<ClientCredentials.IClientsRepository, ClientsRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<AuthorizationEndpoint.IAuthorizationCodesRepository, AuthorizationCodesRepository>();
            services.AddScoped<AuthorizationCode.IAuthorizationCodesRepository, AuthorizationCodesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMapper<ClientEntity, ClientCredentials.Client>, ClientEntityToClientCredentialsClientMapper>();
            services.AddScoped<IMapper<ClientEntity, AuthorizationEndpoint.Client>, ClientEntityToAuthorizationEndpointClientMapper>();
        }

        public static void MigrateScuffedAuth(this IServiceProvider service)
        {
            using var context = service.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}
