namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationRequest
    {

        public AuthorizationRequest(ResponseType responseType, string clientId)
        {
            ResponseType = responseType;
            ClientId = clientId;
        }

        public ResponseType ResponseType { get; }

        public string ClientId { get; }
    }
}
