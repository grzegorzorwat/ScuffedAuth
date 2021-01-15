using System;
using System.Diagnostics.CodeAnalysis;

namespace Authorization.Codes
{
    public abstract class ExpiringCode
    {
        [NotNull] public string? Code { get; set; }

        public DateTime CreationDate { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}
