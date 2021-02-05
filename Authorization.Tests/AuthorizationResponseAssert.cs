using BaseLibrary.Responses;
using FluentAssertions;
using Tests.Library;

namespace Authorization.Tests
{
    public class ResponseAssert
    {
        private readonly Response _response;
        private readonly IResponseVisitor<string> _testResponseVisitor;

        public ResponseAssert(Response response)
        {
            _response = response;
            _testResponseVisitor = new TestResponseVisitor();
        }

        public ResponseAssert HaveRedirectUrl(string url, string because = "")
        {
            string redirectTo = _response.Accept(_testResponseVisitor);
            redirectTo.Split("?")[0].Should().Be(url, because);
            return this;
        }

        public ResponseAssert HaveError(string error, string because = "")
        {
            string redirectTo = _response.Accept(_testResponseVisitor);
            redirectTo.Split("?")[1].Should().Be($"error={error}", because);
            return this;
        }

        public ResponseAssert HaveCode(string because = "")
        {
            string redirectTo = _response.Accept(_testResponseVisitor);
            redirectTo.Split("?")[1].Replace("code=", "").Should().NotBeEmpty(because);
            return this;
        }
    }
}
