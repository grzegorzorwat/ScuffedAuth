using Authentication.ClientCredentials;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests
{
    public class ClientCredentialsTests
    {
        private const string ClientId = "clientId";
        private const string ClientSecret = "clientSecret";
        private const string EncodedClientSecret = "1000.39zyePe+fstN7VVEitrNyg==.fDCT8OLtWjHKhotdLb43EJm0jBehkp6J45NGyMvFYAw=";

        [Fact]
        public async Task ShouldReturnFailureResponseForEmptyHeader()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldReturnSuccessResponseForCorrectCredentials()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();
            string authorizationHeader = GetCorrectClientsCredentialBasicHeader();

            AuthenticationResponse response = await authenticator.Authenticate(authorizationHeader);

            response.Should().BeSuccess();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForIncorrectCredentials()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();
            string authorizationHeader = CreateBasicHeader("incorrectClientId", "incorrectClientSecret");

            AuthenticationResponse response = await authenticator.Authenticate(authorizationHeader);

            response.Should().BeFailure();
        }

        [Theory]
        [MemberData(nameof(GetIncorrectHeaders))]
        public async Task ShouldReturnFailureResponseForIncorrectHeaders(string because, string incorrectHeader)
        {
            IClientsRepository clientsRepository = Substitute.For<IClientsRepository>();
            IAuthenticator authenticator = new ClientCredentialsAuthenticator(new ClientCredentialsDecoder(),
                clientsRepository,
                new SecretVerifier());

            AuthenticationResponse response = await authenticator.Authenticate(incorrectHeader);

            response.Should().BeFailure(because + " was passed");
        }

        private static IAuthenticator GetClientCredentailsAuthenticator()
        {
            IClientsRepository clientsRepository = Substitute.For<IClientsRepository>();
            clientsRepository
                .GetClientByIdAsync(ClientId)
                .Returns(new Client(ClientId, EncodedClientSecret));
            return new ClientCredentialsAuthenticator(new ClientCredentialsDecoder(),
                clientsRepository,
                new SecretVerifier());
        }

        private static string GetCorrectClientsCredentialBasicHeader()
        {
            return CreateBasicHeader(ClientId, ClientSecret);
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
    }
}
