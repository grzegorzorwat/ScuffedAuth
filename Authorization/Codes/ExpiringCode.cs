using System;
using System.Diagnostics.CodeAnalysis;

namespace Authorization.Codes
{
    public abstract class ExpiringCode
    {
        [NotNull] public string? Code { get; init; }

        public DateTime CreationDate { get; init; }

        public TimeSpan ExpiresIn { get; init; }
    }
}
