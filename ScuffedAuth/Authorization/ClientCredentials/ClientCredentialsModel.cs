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

        public static ClientCredentialsModel Empty =>
            new ClientCredentialsModel(string.Empty, string.Empty);
    }
}
