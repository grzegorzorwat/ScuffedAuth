namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationServiceRequest
    {
        public AuthorizationServiceRequest(ResponseType responseType, string clientId, string? redirectUri, string referer)
        {
            ResponseType = responseType;
            ClientId = clientId;
            RedirectUri = redirectUri;
            Referer = referer;
        }

        public ResponseType ResponseType { get; init; }

        public string ClientId { get; init; }

        public string? RedirectUri { get; init; }

        public string Referer { get; init; }
    }
}
