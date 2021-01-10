namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationServiceRequest
    {

        public AuthorizationServiceRequest(ResponseType responseType, string clientId, string? redirectUri)
        {
            ResponseType = responseType;
            ClientId = clientId;
            RedirectUri = redirectUri;
        }

        public ResponseType ResponseType { get; }

        public string ClientId { get; }

        public string? RedirectUri { get; }
    }
}
