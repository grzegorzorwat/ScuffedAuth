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
            RedirectUri = redirectionUri;
        }

        public string Id { get; }

        public string? RedirectUri { get; }
    }
}
