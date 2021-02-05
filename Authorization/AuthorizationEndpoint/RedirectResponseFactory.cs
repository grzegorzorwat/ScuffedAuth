using BaseLibrary.Responses;
using Microsoft.AspNetCore.WebUtilities;

namespace Authorization.AuthorizationEndpoint
{
    public static class RedirectResponseFactory
    {
        public static RedirectResponse WithError(string url, string errorMessage)
        {
            return With(url, "error", errorMessage);
        }

        public static RedirectResponse With(string url, string name, string value)
        {
            return new RedirectResponse(QueryHelpers.AddQueryString(url, name, value));
        }
    }
}
