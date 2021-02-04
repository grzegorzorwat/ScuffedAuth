using Authorization.AuthorizationEndpoint;
using FluentAssertions;

namespace Authorization.Tests
{
    public class AuthorizationResponseAssert
    {
        private readonly AuthorizationResponse _response;

        public AuthorizationResponseAssert(AuthorizationResponse response)
        {
            _response = response;
        }

        public AuthorizationResponseAssert HaveRedirectUrl(string url, string because = "")
        {
            _response.RedirectTo.Split("?")[0].Should().Be(url, because);
            return this;
        }

        public AuthorizationResponseAssert HaveError(string error, string because = "")
        {
            _response.RedirectTo.Split("?")[1].Should().Be($"error={error}", because);
            return this;
        }

        public AuthorizationResponseAssert HaveCode(string because = "")
        {
            _response.RedirectTo.Split("?")[1].Replace("code=", "").Should().NotBeEmpty(because);
            return this;
        }
    }
}
