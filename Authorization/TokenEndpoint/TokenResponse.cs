using BaseLibrary;

namespace Authorization.TokenEndpoint
{
    public class TokenResponse : BaseResponse
    {
        public Token Token { get; set; }

        private TokenResponse(bool success, string message, Token token) : base(success, message)
        {
            Token = token;
        }

        public TokenResponse(string message) : this(false, message, Token.Empty) { }

        public TokenResponse(Token token) : this(true, string.Empty, token) { }
    }
}
