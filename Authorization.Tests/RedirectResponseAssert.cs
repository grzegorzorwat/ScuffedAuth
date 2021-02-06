using BaseLibrary.Responses;
using FluentAssertions;
using Tests.Library;

namespace Authorization.Tests
{
    public class RedirectResponseAssert
    {
        private readonly RedirectResponse _response;

        public RedirectResponseAssert(RedirectResponse response)
        {
            _response = response;
        }

        public RedirectResponseAssert HaveRedirectUrl(string url, string because = "")
        {
            _response.RedirectUrl.Split("?")[0].Should().Be(url, because);
            return this;
        }

        public RedirectResponseAssert HaveError(string error, string because = "")
        {
            _response.RedirectUrl.Split("?")[1].Should().Be($"error={error}", because);
            return this;
        }

        public RedirectResponseAssert HaveCode(string because = "")
        {
            _response.RedirectUrl.Split("?")[1].Replace("code=", "").Should().NotBeEmpty(because);
            return this;
        }
    }
}
