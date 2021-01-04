using Authentication.ClientCredentials;
using System;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCodeGenerator : IAuthorizationCodeGenerator
    {
        public AuthorizationCode Generate(string clientId)
        {
            return new AuthorizationCode("code", clientId, DateTime.UtcNow, 60);
        }
    }
}
