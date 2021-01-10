namespace Authorization
{
    public class AuthorizationRequest
    {
        public GrantTypes GrantType { get; init; }

        public string? Code { get; init; }

        public string? ClientId { get; init; }
    }
}
