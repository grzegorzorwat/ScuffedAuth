using Authentication.ClientCredentials;
using Authorization.AuthorizationEndpoint;
using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Authorization.Tests
{
    public class AuthorizationServiceTests
    {
        [Fact]
        public async Task ShouldReturnFailureResponseForIncorrectResponseType()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationRequest request = new TestAuthorizationRequestBuilder()
            {
                ResponseType = "incorrectResponseType"
            }.Build();

            AuthorizationResponse response = await service.Authorize(request);

            response.Success.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForNotExistingClient()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationRequest request = new TestAuthorizationRequestBuilder()
            {
                ClientId = "NotExistingClientId"
            }.Build();

            AuthorizationResponse response = await service.Authorize(request);

            response.Success.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldReturnSuccessReponseForExistingClient()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationRequest request = new TestAuthorizationRequestBuilder().Build();

            AuthorizationResponse response = await service.Authorize(request);

            response.Success.Should().BeTrue();
        }

        private AuthorizationService GetAuthorizationService()
        {
            IClientsRepository clientsRepository = Substitute.For<IClientsRepository>();
            clientsRepository.GetClientByIdAsync("ClientId").Returns(new Client("ClientId", "ClientSecret"));
            return new AuthorizationService(clientsRepository, new AuthorizationCodeGenerator());
        }

        private class TestAuthorizationRequestBuilder
        {
            public string ResponseType { private get; set; } = "code";
            public string ClientId { private get; set; } = "ClientId";

            public AuthorizationRequest Build()
            {
                return new AuthorizationRequest(ResponseType, ClientId);
            }
        }
    }
}
