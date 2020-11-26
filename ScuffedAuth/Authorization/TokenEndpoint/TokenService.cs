namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenService : ITokenService
    {
        private readonly AuthorizationFactory _authorizationFactory;

        public TokenService(AuthorizationFactory authorizationFactory)
        {
            _authorizationFactory = authorizationFactory;
        }

        public TokenResponse GetToken(string authorizationHeader, TokenRequest request)
        {
            var authorization = _authorizationFactory.GetAuthorization(request.GrantType);
            return authorization.GetToken(authorizationHeader);
        }
    }
}
