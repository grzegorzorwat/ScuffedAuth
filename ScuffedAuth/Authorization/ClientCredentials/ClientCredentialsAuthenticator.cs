namespace ScuffedAuth.Authorization.ClientCredentials
{
    public class ClientCredentialsAuthentication : IClientCredentialsAuthentication
    {
        public bool Authenticate(string clientId, string clientSecret)
        {
            return clientId == "clientId"
                   && clientSecret == "clientSecret";
        }
    }
}
