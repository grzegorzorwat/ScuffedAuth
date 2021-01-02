namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationResponse : BaseResponse
    {
        public AuthorizationCode AuthorizationCode { get; set; }

        private AuthorizationResponse(bool success, string message, AuthorizationCode authorizationCode) : base(success, message)
        {
            AuthorizationCode = authorizationCode;
        }

        public AuthorizationResponse(string message) : this(false, message, default!) { }

        public AuthorizationResponse(AuthorizationCode token) : this(true, string.Empty, token) { }
    }
}
