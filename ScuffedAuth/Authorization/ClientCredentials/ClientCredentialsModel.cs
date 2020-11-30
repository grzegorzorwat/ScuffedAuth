namespace ScuffedAuth.Authorization.ClientCredentials
{
    public class ClientCredentialsModel
    {
        public ClientCredentialsModel(string id, string secret)
        {
            Id = id;
            Secret = secret;
        }

        public string Id { get; }
        public string Secret { get; }
    }
}
