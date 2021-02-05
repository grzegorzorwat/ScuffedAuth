using Authorization.AuthorizationEndpoint;
using Authorization.Codes;
using BaseLibrary.Responses;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Threading.Tasks;
using Xunit;

namespace Authorization.Tests
{
    public class AuthorizationServiceTests
    {
        private const string ExampleUri = "https://www.example.com";
        private const string OtherExampleUri = "https://www.anotherexample.com";

        [Fact]
        public async Task ShouldReturnFailureResponseForIncorrectResponseType()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                ResponseType = ResponseType.unidentified
            }.Build();

            Response response = await service.Authorize(request);

            response.Should().HaveError("unsupported_response_type");
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForNotExistingClient()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                ClientId = "NotExistingClientId"
            }.Build();

            Response response = await service.Authorize(request);

            response.Should().HaveError("unauthorized_client");
        }

        [Fact]
        public async Task ShouldReturnSuccessReponseForExistingClient()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder().Build();

            Response response = await service.Authorize(request);

            response.Should().HaveCode();
        }

        [Fact]
        public async Task ShouldReturnFailureReponseWhenRequestWithoutRedirectUriIsPassedForClientWithoutRedirectUri()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                RedirectUri = null
            }.Build();

            Response response = await service.Authorize(request);

            response.Should().HaveError("invalid_request");
        }

        [Fact]
        public async Task ShouldReturnRedirectionUriFromClientWhenRequestWithoutRedirectUriIsPassed()
        {
            var client = new Client("ClientId", ExampleUri);
            IAuthorizationService service = GetAuthorizationService(client);
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                RedirectUri = null
            }.Build();

            Response response = await service.Authorize(request);

            response.Should().HaveRedirectUrl(ExampleUri);
        }

        [Fact]
        public async Task ShouldReturnRedirectionUriFromRequestWhenRequestWithRedirectUriIsPassedForClientWithoutRedirectUri()
        {
            var client = new Client("ClientId");
            IAuthorizationService service = GetAuthorizationService(client);
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                RedirectUri = ExampleUri
            }.Build();

            Response response = await service.Authorize(request);

            response.Should().HaveRedirectUrl(ExampleUri);
        }

        [Fact]
        public async Task ShouldReturnRedirectionUriFromRequestWhenRequestWithRedirectUriIsPassedForClientWithRedirectUri()
        {
            var client = new Client("ClientId", ExampleUri);
            IAuthorizationService service = GetAuthorizationService(client);
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                RedirectUri = OtherExampleUri
            }.Build();

            Response response = await service.Authorize(request);

            response.Should().HaveRedirectUrl(OtherExampleUri);
        }

        [Theory]
        [InlineData("www.example.com", "uri without schema")]
        [InlineData("example.com", "uri without schema")]
        [InlineData("ftp://www.example.com", "uri with invalid schema (only http and https are valid)")]
        [InlineData("/example/com", "partial uri")]
        public async Task ShouldReturnFailureReponseForInvalidRedirectUri(string invalidRedirectUri, string because)
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                RedirectUri = invalidRedirectUri
            }.Build();

            Response response = await service.Authorize(request);

            response.Should().HaveError("invalid_request", because + " was passed");
        }

        [Fact]
        public async Task ShouldReturnResponseFromAuthenticatorIfAuthenticatorReturnsResponse()
        {
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder().Build();
            var authenticator = Substitute.For<IAuthorizationCodeAuthentication>();
            var expectedResponse = RedirectResponseFactory.With(ExampleUri, "ReturnUri", OtherExampleUri);
            authenticator.Authenticate().Returns(expectedResponse);
            var service = GetAuthorizationService(authenticator: authenticator);

            var response = await service.Authorize(request);

            Assert.Equal(expectedResponse, response);
        }

        private static AuthorizationService GetAuthorizationService(Client clientToSave = default!,
            IAuthorizationCodeAuthentication authenticator = default!)
        {
            var client = clientToSave ?? new Client("ClientId");
            var authorizationCodeAuthentication = authenticator;

            if(authorizationCodeAuthentication is null)
            {
                authorizationCodeAuthentication = Substitute.For<IAuthorizationCodeAuthentication>();
                authorizationCodeAuthentication.Authenticate().ReturnsNull();
            }

            IAuthorizationCodesRepository repository = Substitute.For<IAuthorizationCodesRepository>();
            repository.GetClientByIdAsync(client.Id).Returns(client);
            var settings = Options.Create(new ExpiringCodesGeneratorSettings()
            {
                ExpiresIn = 60,
                Length = 32
            });
            return new AuthorizationService(new AuthorizationCodeGenerator(settings),
                repository,
                Substitute.For<IUnitOfWork>(),
                authorizationCodeAuthentication);
        }

        private class TestAuthorizationRequestBuilder
        {
            public ResponseType ResponseType { private get; set; } = ResponseType.code;

            public string ClientId { private get; set; } = "ClientId";

            public string? RedirectUri { private get; set; } = ExampleUri;

            public AuthorizationServiceRequest Build()
            {
                return new AuthorizationServiceRequest(ResponseType, ClientId, RedirectUri, string.Empty);
            }
        }
    }
}
