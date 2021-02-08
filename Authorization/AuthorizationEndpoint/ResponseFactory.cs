using BaseLibrary.Responses;
using Microsoft.AspNetCore.WebUtilities;

namespace Authorization.AuthorizationEndpoint
{
    public static class ResponseFactory
    {
        public static Response WithError(string url, string errorMessage)
        {
            if (string.IsNullOrEmpty(url))
            {
                return WithError(errorMessage);
            }

            return With(url, "error", errorMessage);
        }

        public static Response WithError(string errorMessage)
        {
            return new ErrorResponse<AuthorizationError>(new AuthorizationError(errorMessage));
        }

        public static RedirectResponse With(string url, string name, string value)
        {
            return new RedirectResponse(QueryHelpers.AddQueryString(url, name, value));
        }
    }
}
