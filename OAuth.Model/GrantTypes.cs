using System.Text.Json.Serialization;

namespace OAuth.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GrantTypes
    {
        unidentified,
        client_credentials,
        authorization_code
    }
}
