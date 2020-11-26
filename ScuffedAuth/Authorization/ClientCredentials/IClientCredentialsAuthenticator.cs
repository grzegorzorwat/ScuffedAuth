namespace ScuffedAuth.Authorization.ClientCredentials
{
    public interface IClientCredentialsAuthenticator
    {
        bool Authenticate(string clientId, string clientSecret);
    }
}
