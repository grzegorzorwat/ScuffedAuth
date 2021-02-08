using System.Text.Json.Serialization;

namespace Authorization.IntrospectionEnpoint
{
    public class TokenInfoResource
    {
        public TokenInfoResource(bool active)
        {
            Active = active;
        }

        [JsonPropertyName("active")]
        public bool Active { get; }

    }
}
