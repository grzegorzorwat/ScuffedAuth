using ScuffedAuth.Authorization.ClientCredentials;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenService : ITokenService
    {
        public TokenResponse GetToken(string authorizationHeader, TokenRequest request)
        {
            if (request.GrantType == GrantTypes.client_credentials)
            {
                var tokenProvider = new ClientCredentialsAuthorization();
                return tokenProvider.GetToken(authorizationHeader);
            }

            return new TokenResponse("Grant type must be defined.");
        }
    }
}
