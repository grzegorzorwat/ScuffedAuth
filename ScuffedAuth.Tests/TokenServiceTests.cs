using FluentAssertions;
using ScuffedAuth.Authorization;
using ScuffedAuth.Authorization.TokenEndpoint;
using Xunit;

namespace ScuffedAuth.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public void GetToken_ForUnidentifiedGrantType_ShouldReturnFailureResponse()
        {
            ITokenService service = new TokenService();
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
            ITokenService service = new TokenService();
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
            ITokenService service = new TokenService();
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.unidentified
            };

            TokenResponse response = service.GetToken(string.Empty, request);

            response.Token.Value.Should().BeEmpty();
        }
    }
}
