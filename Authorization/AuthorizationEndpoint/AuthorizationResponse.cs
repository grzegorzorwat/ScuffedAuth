using System.Net;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationResponse
    {
        private readonly string _redirectUri;

        private readonly string _queryString;

        public AuthorizationResponse(string redirectUri, string queryString)
        {
            _redirectUri = redirectUri;
            _queryString = queryString;
        }

        public string RedirectTo
        {
            get
            {
                return $"{_redirectUri}?{_queryString}";
            }
        }

        public static AuthorizationResponse WithError(string uri, string errorMessage)
        {
            return WithKeyValue(uri, "error", errorMessage);
        }

        public static AuthorizationResponse WithCode(string uri, string code)
        {
            return WithKeyValue(uri, "code", code);
        }

        public static AuthorizationResponse WithKeyValue(string uri, string key, string value)
        {
            return new AuthorizationResponse(uri, $"{WebUtility.UrlEncode(key)}={WebUtility.UrlEncode(value)}");
        }
    }
}
