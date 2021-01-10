using Authorization.AuthorizationCode;
using Authorization.Tests;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Authorization.Tests
{
    public class AuthorizationCodeTests
    {
        private const string CorrectCode = "code";
        private const string CorrectClientId = "ClientId";

        [Fact]
        public async Task ShouldPassForCorrectRequest()
        {
            var request = new AuthorizationRequest()
            {
                Code = CorrectCode,
                ClientId = CorrectClientId
            };
            IAuthorizator authorizator = GetAuthorizator(request);

            bool result = await authorizator.Authorize();

            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldFailForEmptyCode()
        {
            var request = new AuthorizationRequest()
            {
                Code = string.Empty,
                ClientId = CorrectClientId
            };
            IAuthorizator authorizator = GetAuthorizator(request);

            bool result = await authorizator.Authorize();

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldFailForNotExistingCode()
        {
            var request = new AuthorizationRequest()
            {
                Code = "notExistingCode",
                ClientId = CorrectClientId
            };
            IAuthorizator authorizator = GetAuthorizator(request);

            bool result = await authorizator.Authorize();

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldFailForEmptyClientId()
        {
            var request = new AuthorizationRequest()
            {
                Code = CorrectCode,
                ClientId = string.Empty
            };
            IAuthorizator authorizator = GetAuthorizator(request);

            bool result = await authorizator.Authorize();

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldFailForCodeIssuedForAnotherClient()
        {
            var request = new AuthorizationRequest()
            {
                Code = CorrectCode,
                ClientId = "AnotherClientId"
            };
            IAuthorizator authorizator = GetAuthorizator(request);

            bool result = await authorizator.Authorize();

            result.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldFailForExpiredCode()
        {
            IAuthorizationCodesRepository authorzationCodesRepository = Substitute.For<IAuthorizationCodesRepository>();
            authorzationCodesRepository.GetAuthorizationCode(CorrectCode).Returns(new AuthorizationCode.AuthorizationCode()
            {
                Code = CorrectCode,
                ClientId = CorrectClientId,
                CreationDate = DateTime.UtcNow.AddSeconds(-120),
                ExpiresIn = 60
            });
            var request = new AuthorizationRequest()
            {
                Code = CorrectCode,
                ClientId = CorrectClientId
            };
            IAuthorizator authorizator = new AuthorizationCodeAuthorizator(authorzationCodesRepository, request);

            bool result = await authorizator.Authorize();

            result.Should().BeFalse();
        }

        private static IAuthorizator GetAuthorizator(AuthorizationRequest request)
        {
            IAuthorizationCodesRepository authorzationCodesRepository = Substitute.For<IAuthorizationCodesRepository>();
            authorzationCodesRepository.GetAuthorizationCode(CorrectCode).Returns(new AuthorizationCode.AuthorizationCode()
            {
                Code = CorrectCode,
                ClientId = CorrectClientId,
                CreationDate = DateTime.UtcNow,
                ExpiresIn = 60
            });
            return new AuthorizationCodeAuthorizator(authorzationCodesRepository, request);
        }
    }
}
