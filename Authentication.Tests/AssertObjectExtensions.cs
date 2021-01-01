namespace Authentication.Tests
{
    public static class AssertObjectExtensions
    {
        public static AuthenticationResponseAssert Should(this AuthenticationResponse response)
        {
            return new AuthenticationResponseAssert(response);
        }
    }
}
