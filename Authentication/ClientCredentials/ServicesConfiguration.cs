using Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authentication.ClientCredentials
{
    public static class ServicesConfiguration
    {
        public static void AddClientCredentials(this IServiceCollection services)
        {
            services.AddScoped<IClientCredentialsAuthenticator, ClientCredentialsAuthenticator>();
            services.AddScoped<ClientCredentialsAuthorization>()
                .AddScoped<IAuthorization, ClientCredentialsAuthorization>(
                    s => s.GetRequiredService<ClientCredentialsAuthorization>());
            services.AddScoped<ClientCredentialsDecoder>();
            services.AddScoped<ISecretVerifier, SecretVerifier>();
        }

        public static IAuthorization GetClientCredentialsAuthorization(this IServiceProvider serviceProvider)
        {
            return (IAuthorization)serviceProvider.GetRequiredService(typeof(ClientCredentialsAuthorization));
        }
    }
}
