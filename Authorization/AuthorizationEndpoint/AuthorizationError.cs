using System.Text.Json.Serialization;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationError
    {
        public AuthorizationError(string error)
        {
            Error = error;
        }

        [JsonPropertyName("error")]
        public string Error { get; }
    }
}
