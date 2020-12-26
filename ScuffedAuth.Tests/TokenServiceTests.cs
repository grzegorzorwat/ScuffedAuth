using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using ScuffedAuth.Authorization;
using ScuffedAuth.Authorization.ClientCredentials;
using ScuffedAuth.Authorization.TokenEndpoint;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScuffedAuth.Tests
{
    public class TokenServiceTests
    {
        private const string ClientId = "clientId";
        private const string ClientSecret = "clientSecret";
        private const string EncodedClientSecret = "1000.39zyePe+fstN7VVEitrNyg==.fDCT8OLtWjHKhotdLb43EJm0jBehkp6J45NGyMvFYAw=";

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
        public async Task GetToken_ForUnidentifiedGrantTypeWithCorrectCredentials_ShouldReturnFailureResponse()
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.unidentified
            };
            string authorizationHeader = GetCorrectClientsCredentialBasicHeader();

            TokenResponse response = await service.GetToken(authorizationHeader, request);

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
            string authorizationHeader = GetCorrectClientsCredentialBasicHeader();

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
        public async Task GetToken_ForIncorrectClientCredentialsHeaders_ShouldReturnFailureResponse(string because, string incorrectHeader)
        {
            ITokenService service = CreateTokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            TokenResponse response = await service.GetToken(incorrectHeader, request);

            response.Should().BeFailure(because + " was passed");
        }

        [Theory]
        [InlineData(60)]
        [InlineData(3600)]
        public async Task GetToken_ForProvidedExpiresInSetting_ShouldReturnTokenWithCorrectExpiresInValue(int expiresIn)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = expiresIn,
                Length = 32,
                TokenType = "Bearer"
            };
            ITokenService service = CreateTokenService(settings);
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };
            string authorizationHeader = GetCorrectClientsCredentialBasicHeader();

            TokenResponse response = await service.GetToken(authorizationHeader, request);

            response.Token.ExpiresIn.Should().Be(expiresIn);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(32)]
        public async Task GetToken_ForProvidedEvenLengthSetting_ShouldReturnTokenWithCorrectExactLength(int length)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = 60,
                Length = length,
                TokenType = "Bearer"
            };
            ITokenService service = CreateTokenService(settings);
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };
            string authorizationHeader = GetCorrectClientsCredentialBasicHeader();

            TokenResponse response = await service.GetToken(authorizationHeader, request);

            response.Token.Value.Length.Should().Be(length);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void TokenGeneratorSettings_ForLengthLessOrEqualToOne_ShouldReturnValidationError(int length)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = 60,
                Length = length,
                TokenType = "Bearer"
            };

            var validationModel = ValidateModel(settings);

            validationModel.Should().Contain(
                x => x.MemberNames.Contains("Length")
                    && !string.IsNullOrEmpty(x.ErrorMessage)
                    && x.ErrorMessage.Contains("between"));
        }

        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Theory]
        [InlineData(3, 2)]
        [InlineData(5, 4)]
        [InlineData(31, 30)]
        public async Task GetToken_ForProvidedOddLengthSetting_ShouldReturnTokenWithCorrectRoundedDownToEvenLength(int length, int expectedLength)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = 60,
                Length = length,
                TokenType = "Bearer"
            };
            ITokenService service = CreateTokenService(settings);
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };
            string authorizationHeader = GetCorrectClientsCredentialBasicHeader();

            TokenResponse response = await service.GetToken(authorizationHeader, request);

            response.Token.Value.Length.Should().Be(expectedLength);
        }

        [Theory]
        [InlineData("Bearer")]
        [InlineData("OtherTokenType")]
        public async Task GetToken_ForProvidedTokenTypeSetting_ShouldReturnTokenWithCorrectTokenType(string tokenType)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = 60,
                Length = 32,
                TokenType = tokenType
            };
            ITokenService service = CreateTokenService(settings);
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };
            string authorizationHeader = GetCorrectClientsCredentialBasicHeader();

            TokenResponse response = await service.GetToken(authorizationHeader, request);

            response.Token.TokenType.Should().Be(tokenType);
        }

        private static ITokenService CreateTokenService()
        {
            return CreateTokenService(new TokenGeneratorSettings()
            {
                ExpiresIn = 60,
                Length = 32,
                TokenType = "Bearer"
            });
        }

        private static ITokenService CreateTokenService(TokenGeneratorSettings pSettings)
        {
            var factory = Substitute.For<AuthorizationFactory>();
            factory
                .GetAuthorization(GrantTypes.unidentified)
                .Returns(new UnidentifiedAuthorization());
            var clientsRepository = Substitute.For<IClientsRepository>();
            clientsRepository
                .GetClientByIdAsync(ClientId)
                .Returns(new Client(ClientId, EncodedClientSecret));
            var authenticator = new ClientCredentialsAuthenticator(clientsRepository, new SecretVerifier());
            var tokenGeneratorSettings =
                Options.Create(pSettings);
            factory
                .GetAuthorization(GrantTypes.client_credentials)
                .Returns(new ClientCredentialsAuthorization(authenticator,
        new TokenGenerator(tokenGeneratorSettings),
                    new ClientCredentialsDecoder()));
            return new TokenService(factory);
        }

        private static string GetCorrectClientsCredentialBasicHeader()
        {
            return CreateBasicHeader(ClientId, ClientSecret);
        }

        public static IEnumerable<object[]> GetIncorrectHeaders
        {
            get
            {
                return new List<object[]>
                {
                    new object[] {"not encoded header", "Basic clientId:clientSecret"},
                    new object[] {"not basic header", Encode("clientId:clientSecret")},
                    new object[] {"bearer header", $"Bearer {Encode("clientId:clientSecret")}"},
                    new object[] {"header with multiple colons", $"Basic {Encode("clientId:clientSecret:")}"},
                    new object[] {"header with empty clientId", $"Basic {Encode(":clientSecret")}"},
                    new object[] {"header with empty clientSecret", $"Basic {Encode("clientId:")}"}
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
