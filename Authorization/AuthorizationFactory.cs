using Authorization.AuthorizationCode;
using Microsoft.Extensions.DependencyInjection;
using OAuth.Model;
using System;

namespace Authorization
{
    public class AuthorizationFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public AuthorizationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual IAuthorizator GetAuthentication(AuthorizationRequest request)
        {
            return request.GrantType switch
            {
                GrantTypes.authorization_code => new AuthorizationCodeAuthorizator(
                    _serviceProvider.GetRequiredService<IAuthorizationCodesRepository>(),
                    request),
                _ => _serviceProvider.GetRequiredService<PassThroughAuthorizator>()
            };
        }
    }
}
