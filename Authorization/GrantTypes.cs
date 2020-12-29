using System.Text.Json.Serialization;

namespace Authorization
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GrantTypes
    {
        unidentified,
        client_credentials
    }
}
