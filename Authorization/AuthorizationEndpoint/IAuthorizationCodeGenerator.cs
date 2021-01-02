using Authentication.ClientCredentials;

namespace Authorization.AuthorizationEndpoint
{
    internal interface IAuthorizationCodeGenerator
    {
        AuthorizationCode Generate(Client client);
    }
}