using System;
using System.Diagnostics.CodeAnalysis;

namespace Authorization.AuthorizationCode
{
    public class AuthorizationCode
    {
        [NotNull] public string? Code { get; init; }

        [NotNull] public string? ClientId { get; init; }

        public DateTime CreationDate { get; init; }

        public int ExpiresIn { get; init; }

        public bool IsExpired
        {
            get
            {
                return DateTime.UtcNow.CompareTo(CreationDate.AddSeconds(ExpiresIn)) > 0;
            }
        }

        public string? RedirectUri { get; init; }
    }
}
