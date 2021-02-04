namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationServiceRequest
    {
        public AuthorizationServiceRequest(ResponseType responseType, string clientId, string? redirectUri, string callingUri)
        {
            ResponseType = responseType;
            ClientId = clientId;
            RedirectUri = redirectUri;
            Referer = callingUri;
        }

        public ResponseType ResponseType { get; init; }

        public string ClientId { get; init; }

        public string? RedirectUri { get; init; }

        public string Referer { get; set; }
    }
}
