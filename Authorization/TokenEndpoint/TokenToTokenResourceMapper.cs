using BaseLibrary;

namespace Authorization.TokenEndpoint
{
    public class TokenToTokenResourceMapper : IMapper<Token, TokenResource>
    {
        public TokenResource Map(Token source)
        {
            return new TokenResource()
            {
                AccessToken = source.Code,
                ExpiresIn = (int)source.ExpiresIn.TotalSeconds,
                TokenType = source.TokenType
            };
        }
    }
}
