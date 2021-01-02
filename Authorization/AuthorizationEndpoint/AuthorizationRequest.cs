using System.ComponentModel.DataAnnotations;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationRequest
    {
        public AuthorizationRequest(string responseType, string clientId)
        {
            ResponseType = responseType;
            ClientId = clientId;
        }

        [Required]
        public string ResponseType { get; init; }

        [Required]
        public string ClientId { get; init; }
    }
}
