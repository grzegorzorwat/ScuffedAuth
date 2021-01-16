using Authorization.Codes;
using Microsoft.Extensions.Options;

namespace Authorization.TokenEndpoint
{
    public class TokenGenerator : ExpiringCodesGenerator, ITokenGenerator
    {
        private readonly TokenGeneratorSettings _settings;

        public TokenGenerator(IOptions<TokenGeneratorSettings> settings) : base(settings)
        {
            _settings = settings.Value;
        }

        public Token Generate()
        {
            return new Token
            {
                Code = GenerateCode(),
                CreationDate = GetCreationDate(),
                ExpiresIn = GetExpiresIn(),
                TokenType = _settings.TokenType
            };
        }
    }
}
