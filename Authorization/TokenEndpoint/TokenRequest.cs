using System.ComponentModel.DataAnnotations;

namespace Authorization.TokenEndpoint
{
    public class TokenRequest
    {
        [Required]
        public GrantTypes GrantType { get; set; }
    }
}
