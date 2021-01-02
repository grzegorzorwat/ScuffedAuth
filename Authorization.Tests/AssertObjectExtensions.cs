using Authorization.AuthorizationEndpoint;
using Tests.Library;

namespace Authorization.Tests
{
    public static class AssertObjectExtensions
    {
        public static AuthorizationResponseAssert Should(this AuthorizationResponse response)
        {
            return new AuthorizationResponseAssert(response);
        }
    }
}
