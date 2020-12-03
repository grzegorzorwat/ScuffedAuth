using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenService : ITokenService
    {
        private readonly AuthorizationFactory _authorizationFactory;

        public TokenService(AuthorizationFactory authorizationFactory)
        {
            _authorizationFactory = authorizationFactory;
        }

        public async Task<TokenResponse> GetToken(string authorizationHeader, TokenRequest request)
        {
            var authorization = _authorizationFactory.GetAuthorization(request.GrantType);
            return await authorization.GetToken(authorizationHeader);
        }
    }
}
