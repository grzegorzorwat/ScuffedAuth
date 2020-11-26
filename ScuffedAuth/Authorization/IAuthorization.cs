using ScuffedAuth.Authorization.TokenEndpoint;

namespace ScuffedAuth.Authorization
{
    public interface IAuthorization
    {
        TokenResponse GetToken(string authorizationHeader);
    }
}
