using System.Text.Json.Serialization;

namespace ScuffedAuth.Authorization
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GrantTypes
    {
        unidentified,
        client_credentials
    }
}
