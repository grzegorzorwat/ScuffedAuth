using Authorization;

namespace Authentication
{
    public class AuthenticationResponse : BaseResponse
    {
        public ResponseClient Client { get; set; }

        public AuthenticationResponse(bool success, string message, ResponseClient client) : base(success, message)
        {
            Client = client;
        }

        public AuthenticationResponse(string message) : this(false, message, new ResponseClient(string.Empty)) { }

        public AuthenticationResponse(ResponseClient client) : this(true, string.Empty, client) { }
    }
}
