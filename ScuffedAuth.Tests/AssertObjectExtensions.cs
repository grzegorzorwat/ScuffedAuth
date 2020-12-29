using Authorization.TokenEndpoint;

namespace ScuffedAuth.Tests
{
    public static class AssertObjectExtensions
    {
        public static TokenResponseAssert Should(this TokenResponse tokenResponse)
        {
            return new TokenResponseAssert(tokenResponse);
        }
    }
}
