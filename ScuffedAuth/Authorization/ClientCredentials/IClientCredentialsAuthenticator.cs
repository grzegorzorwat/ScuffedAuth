namespace ScuffedAuth.Authorization.ClientCredentials
{
    public interface IClientCredentialsAuthentication
    {
        bool Authenticate(string clientId, string clientSecret);
    }
}
