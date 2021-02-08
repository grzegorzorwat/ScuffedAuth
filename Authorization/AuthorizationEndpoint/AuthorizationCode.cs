using Authorization.Codes;
using System.Diagnostics.CodeAnalysis;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCode : ExpiringCode
    {
        [NotNull] public string? ClientId { get; init; }

        [NotNull] public string? RedirectUri { get; init; }
    }
}
