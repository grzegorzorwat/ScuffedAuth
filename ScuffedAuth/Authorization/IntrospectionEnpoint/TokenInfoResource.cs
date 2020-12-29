using System.Text.Json.Serialization;

namespace ScuffedAuth.Authorization.IntrospectionEnpoint
{
    public class TokenInfoResource
    {
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }
}
