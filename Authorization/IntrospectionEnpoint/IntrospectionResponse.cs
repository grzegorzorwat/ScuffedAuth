using BaseLibrary;

namespace Authorization.IntrospectionEnpoint
{
    public class IntrospectionResponse : BaseResponse
    {
        public TokenInfo TokenInfo { get; set; }

        public IntrospectionResponse(TokenInfo tokenInfo) : this(true, string.Empty, tokenInfo) { }

        public IntrospectionResponse(string message) : this(false, message, TokenInfo.Empty) { }

        private IntrospectionResponse(bool success, string message, TokenInfo tokenInfo) : base(success, message)
        {
            TokenInfo = tokenInfo;
        }
    }
}