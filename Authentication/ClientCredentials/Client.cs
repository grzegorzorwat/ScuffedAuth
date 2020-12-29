namespace Authentication.ClientCredentials
{
    public class Client
    {
        public Client(string id, string secret)
        {
            Id = id;
            Secret = secret;
        }

        public string Id { get; }
        public string Secret { get; }
    }
}
