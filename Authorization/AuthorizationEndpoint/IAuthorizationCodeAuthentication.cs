namespace Authorization.AuthorizationEndpoint
{
    public interface IAuthorizationCodeAuthentication
    {
        AuthorizationResponse? Authenticate();
    }
}
