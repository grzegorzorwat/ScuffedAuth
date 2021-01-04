namespace Authorization.AuthorizationEndpoint
{
    public interface IAuthorizationCodeGenerator
    {
        AuthorizationCode Generate(string clientId);
    }
}