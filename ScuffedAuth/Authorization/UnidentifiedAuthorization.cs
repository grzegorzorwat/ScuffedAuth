using ScuffedAuth.Authorization.TokenEndpoint;

namespace ScuffedAuth.Authorization
{
    public class UnidentifiedAuthorization : IAuthorization
    {
        public TokenResponse GetToken(string authorizationHeader)
        {
            return new TokenResponse("Grant type must be defined.");
        }
    }
}
