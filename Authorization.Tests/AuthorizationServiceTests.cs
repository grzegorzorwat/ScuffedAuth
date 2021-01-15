using Authorization.AuthorizationEndpoint;
using Authorization.Codes;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
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

        [Fact]
        public async Task ShouldReturnFailureReponseWhenRequestWithoutRedirectUriIsPassedForClientWithoutRedirectUri()
        {
            IAuthorizationService service = GetAuthorizationService();
            AuthorizationServiceRequest request = new TestAuthorizationRequestBuilder()
            {
                RedirectUri = null
            }.Build();

            AuthorizationResponse response = await service.Authorize(request);

            response.Should().BeFailure().WithMessage("invalid_request");
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

            AuthorizationResponse response = await service.Authorize(request);

            response.AuthorizationCode.RedirectUri.Should().Be(ExampleUri);
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

            AuthorizationResponse response = await service.Authorize(request);

            response.AuthorizationCode.RedirectUri.Should().Be(ExampleUri);
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

            AuthorizationResponse response = await service.Authorize(request);

            response.AuthorizationCode.RedirectUri.Should().Be(OtherExampleUri);
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

            AuthorizationResponse response = await service.Authorize(request);

            response.Should().BeFailure(because + " was passed").WithMessage("invalid_request");
        }

        private static AuthorizationService GetAuthorizationService()
        {
            return GetAuthorizationService(new Client("ClientId"));
        }

        private static AuthorizationService GetAuthorizationService(Client client)
        {
            IAuthorizationCodesRepository repository = Substitute.For<IAuthorizationCodesRepository>();
            repository.GetClientByIdAsync(client.Id).Returns(client);
            var settings = new ExpiringCodesGeneratorSettings()
            {
                ExpiresIn = 60,
                Length = 32
            };
            var options = Options.Create(settings);
            var generator = new ExpiringCodesGenerator<AuthorizationEndpoint.AuthorizationCode>(options);
            return new AuthorizationService(new AuthorizationCodeGenerator(generator),
                repository,
                Substitute.For<IUnitOfWork>());
        }

        private class TestAuthorizationRequestBuilder
        {
            public ResponseType ResponseType { private get; set; } = ResponseType.code;

            public string ClientId { private get; set; } = "ClientId";

            public string? RedirectUri { private get; set; } = ExampleUri;

            public AuthorizationServiceRequest Build()
            {
                return new AuthorizationServiceRequest(ResponseType, ClientId, RedirectUri);
            }
        }
    }
}
