using Authorization.Codes;
using Microsoft.Extensions.Options;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCodeGenerator : ExpiringCodesGenerator, IAuthorizationCodeGenerator
    {
        public AuthorizationCodeGenerator(IOptions<ExpiringCodesGeneratorSettings> settings) : base(settings) { }

        public AuthorizationCode Generate(string clientId, string redirectUri)
        {
            return new AuthorizationCode()
            {
                Code = GenerateCode(),
                CreationDate = GetCreationDate(),
                ExpiresIn = GetExpiresIn(),
                ClientId = clientId,
                RedirectUri = redirectUri
            };
        }
    }
}
