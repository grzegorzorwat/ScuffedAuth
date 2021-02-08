using Authorization.Codes;
using System.ComponentModel.DataAnnotations;

namespace Authorization.TokenEndpoint
{
    public class TokenGeneratorSettings : ExpiringCodesGeneratorSettings
    {
        [Required]
        public string TokenType { get; set; } = string.Empty;
    }
}
