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

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldAlwaysReturnSuccessResponseForPassThroughAuthenticator()
        {
            IAuthenticator authenticator = new PassThroughAuthenticator();

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty);

            response.Success.Should().BeTrue();
        }
    }
}
