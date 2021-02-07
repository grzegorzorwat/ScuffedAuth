using Authentication.ClientCredentials;
using BaseLibrary.Responses;
using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests
{
    public class ClientCredentialsTests
    {
        [Fact]
        public async Task ShouldReturnFailureResponseForEmptyHeader()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();

            Response response = await authenticator.Authenticate(string.Empty);

            response.ShouldBeFailure();
        }

        [Fact]
        public async Task ShouldReturnSuccessResponseForCorrectCredentials()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();
            string header = TestHeaders.GetCorrectClientsCredentialsBasicHeader();

            Response response = await authenticator.Authenticate(header);

            response.Should().BeOfType<SuccessResponse<ResponseClient>>();
            response.As<SuccessResponse<ResponseClient>>().Payload.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForIncorrectCredentials()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();
            string header = TestHeaders.CreateBasicHeader("incorrectClientId", "incorrectClientSecret");

            Response response = await authenticator.Authenticate(header);

            response.ShouldBeFailure();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForExistingClientWithIncorrectSecretPassed()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();
            string header = TestHeaders.CreateBasicHeader(TestHeaders.ClientId, "incorrectClientSecret");

            Response response = await authenticator.Authenticate(header);

            response.ShouldBeFailure();
        }

        [Theory]
        [ClassData(typeof(IncorrectBasicHeaders))]
        public async Task ShouldReturnFailureResponseForIncorrectHeaders(string because, string incorrectHeader)
        {
            IClientsRepository clientsRepository = Substitute.For<IClientsRepository>();
            IAuthenticator authenticator = new ClientCredentialsAuthenticator(new ClientCredentialsDecoder(),
                clientsRepository,
                new SecretVerifier());

            Response response = await authenticator.Authenticate(incorrectHeader);

            response.ShouldBeFailure(because + " was passed");
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForNullHeader()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();

            Response response = await authenticator.Authenticate(null);

            response.ShouldBeFailure();
        }

        private static IAuthenticator GetClientCredentailsAuthenticator()
        {
            IClientsRepository clientsRepository = Substitute.For<IClientsRepository>();
            clientsRepository
                .GetClientByIdAsync(TestHeaders.ClientId)
                .Returns(new Client(TestHeaders.ClientId, TestHeaders.EncodedClientSecret));
            return new ClientCredentialsAuthenticator(new ClientCredentialsDecoder(),
                clientsRepository,
                new SecretVerifier());
        }
    }
}
