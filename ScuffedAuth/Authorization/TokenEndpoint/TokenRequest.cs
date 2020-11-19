using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenRequest
    {
        [Required]
        public GrantTypes GrantType { get; set; }
    }
}
