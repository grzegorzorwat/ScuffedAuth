namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationServiceRequest
    {

        public AuthorizationServiceRequest(ResponseType responseType, string clientId)
        {
            ResponseType = responseType;
            ClientId = clientId;
        }

        public ResponseType ResponseType { get; }

        public string ClientId { get; }
    }
}
