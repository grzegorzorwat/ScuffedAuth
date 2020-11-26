using FluentAssertions;
using ScuffedAuth.Authorization;
using ScuffedAuth.Authorization.TokenEndpoint;
using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using ScuffedAuth.Authorization.ClientCredentials;
using Xunit;

namespace ScuffedAuth.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public void GetToken_ForUnidentifiedGrantType_ShouldReturnFailureResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.unidentified
            };

            TokenResponse response = service.GetToken(string.Empty, request);

            response.Success.Should().BeFalse();
        }

        [Fact]
        public void GetToken_ForUnidentifiedGrantType_ShouldReturnResponseWithErrorMessage()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.unidentified
            };

            TokenResponse response = service.GetToken(string.Empty, request);

            response.Message.Should().NotBeEmpty();
        }

        [Fact]
        public void GetToken_ForUnidentifiedGrantType_ShouldReturnResponseWithEmptyToken()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.unidentified
            };

            TokenResponse response = service.GetToken(string.Empty, request);

            response.Token.Value.Should().BeEmpty();
        }

        [Fact]
        public void GetToken_ForEmptyClientCredentialsHeader_ShouldReturnFailureResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            TokenResponse response = service.GetToken(string.Empty, request);

            response.Success.Should().BeFalse();
        }

        [Fact]
        public void GetToken_ForCorrectClientCredentials_ShouldReturnSuccessResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };
            string authorizationHeader = CreateBasicHeader("clientId", "clientSecret");

            TokenResponse response = service.GetToken(authorizationHeader, request);

            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetToken_ForIncorrectClientCredentials_ShouldReturnFailureResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };
            string authorizationHeader = CreateBasicHeader("incorrectClientId", "incorrectClientSecret");

            TokenResponse response = service.GetToken(authorizationHeader, request);

            response.Success.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(GetIncorrectHeaders))]
        public void GetToken_ForIncorrectClientCredentialsHeaders_ShouldReturnFailureResponse(string testCase, string incorrectHeader)
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            TokenResponse response = service.GetToken(incorrectHeader, request);

            response.Success.Should().BeFalse();
        }

        private ITokenService CreateTokenService()
        {
            var factory = Substitute.For<AuthorizationFactory>();
            factory
                .GetAuthorization(GrantTypes.unidentified)
                .Returns(new UnidentifiedAuthorization());
            var tokenGenerator = Substitute.For<ITokenGenerator>();
            tokenGenerator
                .Generate()
                .Returns(new Token("token"));
            var authenticator = Substitute.For<IClientCredentialsAuthenticator>();
            authenticator
                .Authenticate(Arg.Any<string>(), Arg.Any<string>())
                .Returns(args => (string)args[0] == "clientId" && (string)args[1] == "clientSecret");
            factory
                .GetAuthorization(GrantTypes.client_credentials)
                .Returns(new ClientCredentialsAuthorization(authenticator, tokenGenerator));
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
