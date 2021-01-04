using System.ComponentModel.DataAnnotations;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationRequest
    {
        [Required]
        public string? ResponseType { get; set; }

        [Required]
        public string? ClientId { get; set; }
    }
}
