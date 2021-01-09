using System.Text.Json.Serialization;

namespace Authorization.AuthorizationEndpoint
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResponseType
    {
        unidentified, code
    }
}
