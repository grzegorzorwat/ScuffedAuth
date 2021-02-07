using BaseLibrary.Responses;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests
{
    public class OtherAuthenticationsTests
    {
        [Fact]
        public async Task ShouldAlwaysReturnFailureResponseForUnidentifiedAuthentication()
        {
            IAuthenticator authenticator = new UnidentifiedAuthentication();

            ErrorResponse<string> response = (ErrorResponse<string>)await authenticator.Authenticate(string.Empty);

            response.Payload.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldAlwaysReturnSuccessResponseForPassThroughAuthenticator()
        {
            IAuthenticator authenticator = new PassThroughAuthenticator();

            Response response = await authenticator.Authenticate(string.Empty);

            response.Should().BeOfType<SuccessResponse>();
        }
    }
}
