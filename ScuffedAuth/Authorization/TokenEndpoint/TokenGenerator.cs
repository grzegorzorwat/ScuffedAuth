using System.Linq;
using System.Security.Cryptography;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenGenerator : ITokenGenerator
    {
        public Token Generate()
        {
            using var cryptoServiceProvider = new RNGCryptoServiceProvider();
            var bytes = new byte[16];
            cryptoServiceProvider.GetNonZeroBytes(bytes);
            return new Token(string.Concat(bytes.Select(b => b.ToString("x2"))));
        }
    }
}
