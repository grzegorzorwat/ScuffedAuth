using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Authorization.IntrospectionEnpoint
{
    public class IntrospectionRequest
    {
        [Required]
        public string Token { get; set; } = default!;
    }
}
