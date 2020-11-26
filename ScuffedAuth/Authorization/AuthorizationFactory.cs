using Microsoft.Extensions.DependencyInjection;
using ScuffedAuth.Authorization.ClientCredentials;
using System;

namespace ScuffedAuth.Authorization
{
    public class AuthorizationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        protected AuthorizationFactory() {}

        public AuthorizationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual IAuthorization GetAuthorization(GrantTypes grantType)
        {
            return grantType switch
            {
                GrantTypes.client_credentials => (IAuthorization)_serviceProvider.GetRequiredService(typeof(ClientCredentialsAuthorization)),
                _ => (IAuthorization)_serviceProvider.GetRequiredService(typeof(UnidentifiedAuthorization))
            };
        }
    }
}
