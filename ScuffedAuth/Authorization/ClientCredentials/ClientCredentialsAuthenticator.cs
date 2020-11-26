namespace ScuffedAuth.Authorization.ClientCredentials
{
    public class ClientCredentialsAuthenticator : IClientCredentialsAuthenticator
    {
        public bool Authenticate(string clientId, string clientSecret)
        {
            return clientId == "clientId"
                   && clientSecret == "clientSecret";
        }
    }
}
