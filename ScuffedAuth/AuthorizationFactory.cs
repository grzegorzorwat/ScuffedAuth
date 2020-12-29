using Authentication.ClientCredentials;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authorization
{
    public class AuthorizationFactory : IAuthorizationFactory
    {
        private readonly IServiceProvider _serviceProvider = default!;

        protected AuthorizationFactory() { }

        public AuthorizationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual IAuthorization GetAuthorization(GrantTypes grantType)
        {
            return grantType switch
            {
                GrantTypes.client_credentials => _serviceProvider.GetClientCredentialsAuthorization(),
                _ => (IAuthorization)_serviceProvider.GetRequiredService(typeof(UnidentifiedAuthorization))
            };
        }
    }
}
