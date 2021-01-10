using System;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCodeGenerator : IAuthorizationCodeGenerator
    {
        public AuthorizationCode Generate(string clientId, string redirectionUri)
        {
            return new AuthorizationCode("code", clientId, DateTime.UtcNow, 60, redirectionUri);
        }
    }
}
