using System.Text.Json.Serialization;

namespace Authorization.IntrospectionEnpoint
{
    public class TokenInfoResource
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }
}
