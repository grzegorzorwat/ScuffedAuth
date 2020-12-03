using Microsoft.Extensions.Options;
using NSubstitute;
using ScuffedAuth.Authorization;
using ScuffedAuth.Authorization.ClientCredentials;
using ScuffedAuth.Authorization.TokenEndpoint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScuffedAuth.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public async Task GetToken_ForUnidentifiedGrantType_ShouldReturnFailureResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.unidentified
            };

            TokenResponse response = await service.GetToken(string.Empty, request);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task GetToken_ForEmptyClientCredentialsHeader_ShouldReturnFailureResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            TokenResponse response = await service.GetToken(string.Empty, request);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task GetToken_ForCorrectClientCredentials_ShouldReturnSuccessResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };
            string authorizationHeader = CreateBasicHeader("clientId", "clientSecret");

            TokenResponse response = await service.GetToken(authorizationHeader, request);

            response.Should().BeSuccess();
        }

        [Fact]
        public async Task GetToken_ForIncorrectClientCredentials_ShouldReturnFailureResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };
            string authorizationHeader = CreateBasicHeader("incorrectClientId", "incorrectClientSecret");

            TokenResponse response = await service.GetToken(authorizationHeader, request);

            response.Should().BeFailure();
        }

        [Theory]
        [MemberData(nameof(GetIncorrectHeaders))]
        public async Task GetToken_ForIncorrectClientCredentialsHeaders_ShouldReturnFailureResponse(string testCase, string incorrectHeader)
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            TokenResponse response = await service.GetToken(incorrectHeader, request);

            response.Should().BeFailure();
        }

        private ITokenService CreateTokenService()
        {
            var factory = Substitute.For<AuthorizationFactory>();
            factory
                .GetAuthorization(GrantTypes.unidentified)
                .Returns(new UnidentifiedAuthorization());
            var authenticator = Substitute.For<IClientCredentialsAuthenticator>();
            authenticator
                .Authenticate(Arg.Any<string>(), Arg.Any<string>())
                .Returns(args => (string)args[0] == "clientId" && (string)args[1] == "clientSecret");
            var tokenGeneratorSettings = Options.Create(new TokenGeneratorSettings()
            {
                ExpiresIn = 60,
                Length = 32,
                TokenType = "Bearer"
            });
            factory
                .GetAuthorization(GrantTypes.client_credentials)
                .Returns(new ClientCredentialsAuthorization(authenticator, new TokenGenerator(tokenGeneratorSettings), new ClientCredentialsDecoder()));
            return new TokenService(factory);
        }

        public static IEnumerable<object[]> GetIncorrectHeaders
        {
            get
            {
                return new List<object[]>
                {
                    new object[] {"NotEncodedHeader", "Basic clientId:clientSecret"},
                    new object[] {"NotBasicHeader", Encode("clientId:clientSecret")},
                    new object[] {"BearerHeader", $"Bearer {Encode("clientId:clientSecret")}"},
                    new object[] {"HeaderWithMultipleColons", $"Basic {Encode("clientId:clientSecret:")}"},
                    new object[] {"HeaderWithEmptyClientId", $"Basic {Encode(":clientSecret")}"},
                    new object[] {"HeaderWithEmptyClientSecret", $"Basic {Encode("clientId:")}"}
                };
            }
        }

        private static string CreateBasicHeader(string clientId, string clientSecret)
        {
            string encoded = Encode($"{clientId}:{clientSecret}");
            return $"Basic {encoded}";
        }

        private static string Encode(string source)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(source));
        }
    }
}
