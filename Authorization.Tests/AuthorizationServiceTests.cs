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
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                ResponseType = ResponseType.unidentified
            }.Build();

            AuthorizationResponse response = await service.Authorize(request);

            response.Should().BeFailure().WithMessage("unsupported_response_type");
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForNotExistingClient()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
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
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder().Build();

            AuthorizationResponse response = await service.Authorize(request);

            response.Should().BeSuccess();
        }

        private static AuthorizationService GetAuthorizationService()
        {
            IAuthorizationCodesRepository repository = Substitute.For<IAuthorizationCodesRepository>();
            repository.GetClientByIdAsync("ClientId").Returns(new Client("ClientId"));
            return new AuthorizationService(new AuthorizationCodeGenerator(),
                repository,
                Substitute.For<IUnitOfWork>());
        }

        private class TestAuthorizationRequestBuilder
        {
            public ResponseType ResponseType { private get; set; } = ResponseType.code;
            public string ClientId { private get; set; } = "ClientId";

            public AuthorizationServiceRequest Build()
            {
                return new AuthorizationServiceRequest(ResponseType, ClientId);
            }
        }
    }
}
