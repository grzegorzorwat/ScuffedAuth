using Authorization;
using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Requests
{
    public class TokenRequest
    {
        [Required]
        public GrantTypes GrantType { get; set; }
    }
}
