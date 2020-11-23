using ScuffedAuth.Authorization.TokenEndpoint;

namespace ScuffedAuth.Authorization
{
    public class TokenService : ITokenService
    {
        public TokenResponse GetToken(string authorizationHeader, TokenRequest request)
        {
            if (request.GrantType == GrantTypes.unidentified)
            {
                return new TokenResponse("Grant type must be defined.");
            }

            return new TokenResponse(new Token("token"));
        }
    }
}
