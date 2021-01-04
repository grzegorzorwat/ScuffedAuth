namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCodeResource
    {
        public AuthorizationCodeResource(string code)
        {
            Code = code;
        }

        public string Code { get; init; }
    }
}
