using System.Web;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCodeResource
    {
        public AuthorizationCodeResource(string code)
        {
            Code = code;
        }

        public string Code { get; init; }

        public string ToQueryString()
        {
            return $"?code={HttpUtility.UrlEncode(Code)}";
        }
    }
}
