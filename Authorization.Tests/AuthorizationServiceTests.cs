using Authentication.ClientCredentials;
using Authorization.AuthorizationEndpoint;
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

            response.Should().BeFailure().WithMessage("unsupported_response_type");
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

            response.Should().BeFailure().WithMessage("unauthorized_client");
        }

        [Fact]
        public async Task ShouldReturnSuccessReponseForExistingClient()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationRequest request = new TestAuthorizationRequestBuilder().Build();

            AuthorizationResponse response = await service.Authorize(request);

            response.Should().BeSuccess();
        }

        private static AuthorizationService GetAuthorizationService()
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
