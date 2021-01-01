using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Authorization.TokenEndpoint
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenGeneratorSettings _options;

        public TokenGenerator(IOptions<TokenGeneratorSettings> options)
        {
            _options = options.Value;
        }

        public Token Generate()
        {
            using var cryptoServiceProvider = new RNGCryptoServiceProvider();
            var bytes = new byte[_options.Length / 2];
            cryptoServiceProvider.GetNonZeroBytes(bytes);
            return new Token(string.Concat(bytes.Select(b => b.ToString("x2"))),
                _options.TokenType,
                DateTime.UtcNow,
                _options.ExpiresIn);
        }
    }
}
