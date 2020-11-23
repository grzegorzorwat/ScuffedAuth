namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public interface ITokenService
    {
        TokenResponse GetToken(string authorizationHeader, TokenRequest request);
    }
}
