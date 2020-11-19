using System.Text.Json.Serialization;

namespace ScuffedAuth.Authorization
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GrantTypes
    {
        client_credentials
    }
}
