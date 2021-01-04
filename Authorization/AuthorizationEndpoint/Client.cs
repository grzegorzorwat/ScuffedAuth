using System.Collections.Generic;

namespace Authorization.AuthorizationEndpoint
{
    public class Client
    {
        public Client(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
