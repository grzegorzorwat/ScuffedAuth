using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenGeneratorSettings
    {
        [Required]
        public int ExpiresIn { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public string TokenType { get; set; } = string.Empty;
    }
}
