using System.ComponentModel.DataAnnotations;

namespace Authorization.IntrospectionEnpoint
{
    public class IntrospectionRequest
    {
        [Required]
        public string Token { get; set; } = default!;
    }
}
