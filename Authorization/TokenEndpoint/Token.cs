using Authorization.Codes;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Authorization.TokenEndpoint
{
    public class Token : ExpiringCode
    {
        [NotNull] public string? TokenType { get; init; }

        public static Token Empty => new Token()
        {
            Code = string.Empty,
            CreationDate = DateTime.UtcNow,
            ExpiresIn = TimeSpan.Zero,
            TokenType = string.Empty
        };
    }
}
