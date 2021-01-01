using Authentication.ClientCredentials;
using Authorization;

namespace Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public Client Client { get; set; }

        public AuthenticationResponse(bool success, string message, Client client) : base(success, message)
        {
            Client = client;
        }

        public AuthenticationResponse(string message) : this(false, message, new Client(string.Empty, string.Empty)) { }

        public AuthenticationResponse(Client client) : this(true, string.Empty, client) { }
    }
}
