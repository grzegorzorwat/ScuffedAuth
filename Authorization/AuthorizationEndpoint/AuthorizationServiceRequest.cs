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

        public ResponseType ResponseType { get; init; }

        public string ClientId { get; init; }

        public string? RedirectUri { get; init; }
    }
}
