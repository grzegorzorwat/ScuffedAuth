using Authorization.IntrospectionEnpoint;
using Authorization.TokenEndpoint;
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

            response.Success.Should().BeTrue();
            response.TokenInfo.Active.Should().BeTrue();
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

            response.Success.Should().BeTrue();
            response.TokenInfo.Active.Should().BeFalse();
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

            response.Success.Should().BeFalse();
            response.Message.Should().NotBeEmpty();
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

            response.Success.Should().BeTrue();
            response.TokenInfo.Active.Should().BeFalse();
        }

        private static Task<Token> AnActiveToken(string token)
        {
            return Task.FromResult(new Token(token, "", DateTime.UtcNow, 3600));
        }

        private static Task<Token> AnInacriveToken(string token)
        {
            return Task.FromResult(new Token(token, "", DateTime.UtcNow.AddSeconds(-7200), 3600));
        }
    }
}
