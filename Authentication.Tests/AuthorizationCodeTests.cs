using Authentication.AuthorizationCode;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests
{
    public class AuthorizationCodeTests
    {
        private const string CorrectCode = "code";
        private const string CorrectClientId = "ClientId";

        [Fact]
        public async Task ShouldReturnSuccessResponseForCorrectquery()
        {
            IAuthenticator authenticator = GetAuthenticator();
            string query = Createquery(CorrectCode, CorrectClientId);

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, query);

            response.Should().BeSuccess();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForEmptyCode()
        {
            IAuthenticator authenticator = GetAuthenticator();
            string query = Createquery(string.Empty, CorrectClientId);

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, query);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForNotExistingCode()
        {
            IAuthenticator authenticator = GetAuthenticator();
            string query = Createquery("notExistingCode", CorrectClientId);

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, query);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForEmptyClientId()
        {
            IAuthenticator authenticator = GetAuthenticator();
            string query = Createquery(CorrectCode, string.Empty);

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, query);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForCodeIssuedForAnotherClient()
        {
            IAuthenticator authenticator = GetAuthenticator();
            string query = Createquery(CorrectCode, "AnotherClientId");

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, query);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldReturnFailureResponseForExpiredCode()
        {
            IAuthorizationCodesRepository authorzationCodesRepository = Substitute.For<IAuthorizationCodesRepository>();
            authorzationCodesRepository.GetAuthorizationCode(CorrectCode).Returns(new AuthorizationCode.AuthorizationCode()
            {
                Code = CorrectCode,
                ClientId = CorrectClientId,
                CreationDate = DateTime.UtcNow.AddSeconds(-120),
                ExpiresIn = 60
            });
            IAuthenticator authenticator = new AuthorizationCodeAuthenticator(authorzationCodesRepository);
            string query = Createquery(CorrectCode, CorrectClientId);

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, query);

            response.Should().BeFailure();
        }

        [Fact]
        public async Task ShouldReturnResponseWithSameClientIdAsInquery()
        {
            IAuthenticator authenticator = GetAuthenticator();
            string query = Createquery(CorrectCode, CorrectClientId);

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, query);

            response.Client.Id.Should().Be(CorrectClientId);
        }

        [Theory]
        [MemberData(nameof(GetIncorrectquerys))]
        public async Task ShouldReturnFailureResponseForIncorrectquerys(string because, string incorrectquery)
        {
            IAuthenticator authenticator = GetAuthenticator();
            string query = incorrectquery;

            AuthenticationResponse response = await authenticator.Authenticate(string.Empty, query);

            response.Should().BeFailure(because);
        }

        public static IEnumerable<object[]> GetIncorrectquerys()
        {
            yield return new object[] { "empty query", string.Empty };
            yield return new object[] { "query without code key and value", $"client_id={CorrectClientId}" };
            yield return new object[] { "query without code key", $"{CorrectCode}&client_id={CorrectClientId}" };
            yield return new object[] { "query without clientId key and value", $"code={CorrectCode}" };
            yield return new object[] { "query without clientId key", $"code={CorrectCode}&{CorrectClientId}" };
        }

        private static IAuthenticator GetAuthenticator()
        {
            IAuthorizationCodesRepository authorzationCodesRepository = Substitute.For<IAuthorizationCodesRepository>();
            authorzationCodesRepository.GetAuthorizationCode(CorrectCode).Returns(new AuthorizationCode.AuthorizationCode()
            {
                Code = CorrectCode,
                ClientId = CorrectClientId,
                CreationDate = DateTime.UtcNow,
                ExpiresIn = 60
            });
            return new AuthorizationCodeAuthenticator(authorzationCodesRepository);
        }

        private static string Createquery(string code, string clientId)
        {
            return $"code={code}&client_id={clientId}";
        }
    }
}
