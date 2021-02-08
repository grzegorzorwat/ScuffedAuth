using Authentication.ClientCredentials;
using Microsoft.Extensions.DependencyInjection;
using OAuth.Model;
using System;

namespace Authentication
{
    public class AuthenticationFactory
    {
        private readonly IServiceProvider _serviceProvider = default!;

        public AuthenticationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual IAuthenticator GetAuthentication(GrantTypes grantType)
        {
            return grantType switch
            {
                GrantTypes.client_credentials => _serviceProvider.GetRequiredService<ClientCredentialsAuthenticator>(),
                GrantTypes.authorization_code => _serviceProvider.GetRequiredService<PassThroughAuthenticator>(),
                _ => _serviceProvider.GetRequiredService<UnidentifiedAuthentication>()
            };
        }
    }
}
