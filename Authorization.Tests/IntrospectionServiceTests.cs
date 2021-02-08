using Authorization.IntrospectionEnpoint;
using Authorization.TokenEndpoint;
using BaseLibrary.Responses;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ScuffedAuth.Tests
{
    public class IntrospectionServiceTests
    {
        [Theory]
        [InlineData("activeToken")]
        [InlineData("anotherActiveToken")]
        public async Task ShouldReturnActiveForActiveTokenAsync(string token)
        {
            var repository = Substitute.For<ITokenRepository>();
            repository.GetToken(token).Returns(AnActiveToken(token));
            var service = new IntrospectionService(repository);
            var request = new IntrospectionRequest()
            {
                Token = token
            };

            var response = await service.Introspect(request);

            response.Should().BeOfType<SuccessResponse<TokenInfoResource>>();
            response.As<SuccessResponse<TokenInfoResource>>().Payload.Active.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnInactiveForInactiveTokenAsync()
        {
            const string inactiveToken = "inactiveToken";
            var repository = Substitute.For<ITokenRepository>();
            repository.GetToken(inactiveToken).Returns(AnInacriveToken(inactiveToken));
            var service = new IntrospectionService(repository);
            var request = new IntrospectionRequest()
            {
                Token = inactiveToken
            };

            var response = await service.Introspect(request);

            response.Should().BeOfType<SuccessResponse<TokenInfoResource>>();
            response.As<SuccessResponse<TokenInfoResource>>().Payload.Active.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldNotPassForEmptyTokenAsync()
        {
            var service = new IntrospectionService(Substitute.For<ITokenRepository>());
            var request = new IntrospectionRequest()
            {
                Token = string.Empty
            };

            var response = await service.Introspect(request);

            response.Should().BeOfType<ErrorResponse>();
            response.As<ErrorResponse>().Message.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldReturnInactiveForNotExistingTokenAsync()
        {
            var service = new IntrospectionService(Substitute.For<ITokenRepository>());
            var request = new IntrospectionRequest()
            {
                Token = "notExistingToken"
            };

            var response = await service.Introspect(request);

            response.Should().BeOfType<SuccessResponse<TokenInfoResource>>();
            response.As<SuccessResponse<TokenInfoResource>>().Payload.Active.Should().BeFalse();
        }

        private static Task<Token> AnActiveToken(string token)
        {
            return Task.FromResult(new Token()
            {
                Code = token,
                TokenType = string.Empty,
                CreationDate = DateTime.UtcNow,
                ExpiresIn = TimeSpan.FromSeconds(3600)
            });
        }

        private static Task<Token> AnInacriveToken(string token)
        {
            return Task.FromResult(new Token()
            {
                Code = token,
                TokenType = string.Empty,
                CreationDate = DateTime.UtcNow.AddSeconds(-7200),
                ExpiresIn = TimeSpan.FromSeconds(3600)
            });
        }
    }
}
