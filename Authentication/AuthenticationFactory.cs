using Authentication.AuthorizationCode;
using Authentication.ClientCredentials;
using Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authentication
{
    public class AuthenticationFactory
    {
        private readonly IServiceProvider _serviceProvider = default!;

        protected AuthenticationFactory() { }

        public AuthenticationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual IAuthenticator GetAuthentication(GrantTypes grantType)
        {
            return grantType switch
            {
                GrantTypes.client_credentials => (IAuthenticator)_serviceProvider.GetRequiredService(typeof(ClientCredentialsAuthenticator)),
                GrantTypes.code => _serviceProvider.GetRequiredService<AuthorizationCodeAuthenticator>(),
                _ => (IAuthenticator)_serviceProvider.GetRequiredService(typeof(UnidentifiedAuthentication))
            };
        }
    }
}
