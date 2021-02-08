using BaseLibrary.Responses;

namespace Authorization.AuthorizationEndpoint
{
    public interface IAuthorizationCodeAuthentication
    {
        Response? Authenticate();
    }
}
