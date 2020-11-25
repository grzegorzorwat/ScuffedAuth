namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenGenerator : ITokenGenerator
    {
        public Token Generate()
        {
            return new Token("token");
        }
    }
}
