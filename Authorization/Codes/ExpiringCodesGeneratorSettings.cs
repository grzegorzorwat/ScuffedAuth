using System;
using System.ComponentModel.DataAnnotations;

namespace Authorization.Codes
{
    public class ExpiringCodesGeneratorSettings
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ExpiresIn { get; set; }

        [Required]
        [Range(2, int.MaxValue)]
        public int Length { get; set; }
    }
}
