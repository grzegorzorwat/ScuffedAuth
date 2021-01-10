﻿using Authentication.ClientCredentials;
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

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, string.Empty);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldReturnSuccessResponseForCorrectCredentials()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();
            string header = TestHeaders.GetCorrectClientsCredentialsBasicHeader();

            AuthenticationResponse response = await authenticator.Authenticate(header, string.Empty);

            response.Should().BeSuccess();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForIncorrectCredentials()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();
            string header = TestHeaders.CreateBasicHeader("incorrectClientId", "incorrectClientSecret");

            AuthenticationResponse response = await authenticator.Authenticate(header, string.Empty);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForExistingClientWithIncorrectSecretPassed()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();
            string header = TestHeaders.CreateBasicHeader(TestHeaders.ClientId, "incorrectClientSecret");

            AuthenticationResponse response = await authenticator.Authenticate(header, string.Empty);

            response.Should().BeFailure();
        }

        [Theory]
        [ClassData(typeof(IncorrectBasicHeaders))]
        public async Task ShouldReturnFailureResponseForIncorrectHeaders(string because, string incorrectHeader)
        {
            IClientsRepository clientsRepository = Substitute.For<IClientsRepository>();
            IAuthenticator authenticator = new ClientCredentialsAuthenticator(new ClientCredentialsDecoder(),
                clientsRepository,
                new SecretVerifier());

            AuthenticationResponse response = await authenticator.Authenticate(incorrectHeader);

            response.Should().BeFailure(because + " was passed");
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForNullHeader()
        {
            IAuthenticator authenticator = GetClientCredentailsAuthenticator();

            AuthenticationResponse response = await authenticator.Authenticate(null, string.Empty);

            response.Should().BeFailure();
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
