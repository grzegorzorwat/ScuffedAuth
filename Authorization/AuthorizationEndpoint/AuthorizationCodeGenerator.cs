using Authentication.ClientCredentials;
using System;

namespace Authorization.AuthorizationEndpoint
{
    internal class AuthorizationCodeGenerator : IAuthorizationCodeGenerator
    {
        public AuthorizationCode Generate(Client client)
        {
            return new AuthorizationCode("code", client, DateTime.UtcNow, 60);
        }
    }
}
