using Authentication.ClientCredentials;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authentication
{
    public static class ServicesConfiguration
    {
        public static void AddAuthentication(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationFactory>();
            services
                .AddScoped<UnidentifiedAuthentication>()
                .AddScoped<IAuthenticator, UnidentifiedAuthentication>(
                    s => s.GetRequiredService<UnidentifiedAuthentication>());
            services.AddScoped<ClientCredentialsAuthenticator>()
                .AddScoped<IAuthenticator, ClientCredentialsAuthenticator>(
                    s => s.GetRequiredService<ClientCredentialsAuthenticator>());
            services.AddScoped<ClientCredentialsDecoder>();
            services.AddScoped<ISecretVerifier, SecretVerifier>();
            services
                .AddScoped<PassThroughAuthenticator>()
                .AddScoped<IAuthenticator, PassThroughAuthenticator>(
                    s => s.GetRequiredService<PassThroughAuthenticator>());
        }

        public static IAuthenticator GetClientCredentialsAuthorization(this IServiceProvider serviceProvider)
        {
            return (IAuthenticator)serviceProvider.GetRequiredService(typeof(ClientCredentialsAuthenticator));
        }
    }
}
