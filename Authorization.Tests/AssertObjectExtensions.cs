using Authorization.AuthorizationEndpoint;
using BaseLibrary.Responses;

namespace Authorization.Tests
{
    public static class AssertObjectExtensions
    {
        public static RedirectResponseAssert Should(this RedirectResponse response)
        {
            return new RedirectResponseAssert(response);
        }

        public static AuthorizationErrorResponseAssert Should(this ErrorResponse<AuthorizationError> response)
        {
            return new AuthorizationErrorResponseAssert(response);
        }

        public static RedirectResponse AsRedirectResponse(this Response response)
        {
            return (RedirectResponse)response;
        }

        public static ErrorResponse<AuthorizationError> AsAuthorizationErrorResponse(this Response response)
        {
            return (ErrorResponse<AuthorizationError>)response;
        }
    }
}
