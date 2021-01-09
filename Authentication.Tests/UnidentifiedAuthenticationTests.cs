using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests
{
    public class UnidentifiedAuthenticationTests
    {
        [Fact]
        public async Task ShouldAlwaysReturnFailureResponse()
        {
            IAuthenticator authenticator = new UnidentifiedAuthentication();
            string header = TestHeaders.GetCorrectClientsCredentialsBasicHeader();

            AuthenticationResponse response = await authenticator.Authenticate(header, string.Empty);

            response.Should().BeFailure();
        }
    }
}
