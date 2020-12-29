using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenGeneratorSettings
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ExpiresIn { get; set; }

        [Required]
        [Range(2, int.MaxValue)]
        public int Length { get; set; }

        [Required]
        public string TokenType { get; set; } = string.Empty;
    }
}
