namespace Authorization.AuthorizationEndpoint
{
    public class Client
    {
        public Client(string id)
        {
            Id = id;
        }

        public Client(string id, string? redirectionUri)
        {
            Id = id;
            RedirectionUri = redirectionUri;
        }

        public string Id { get; }

        public string? RedirectionUri { get; }
    }
}
