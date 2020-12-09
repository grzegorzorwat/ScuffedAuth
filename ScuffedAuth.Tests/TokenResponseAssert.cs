using FluentAssertions;
using ScuffedAuth.Authorization.TokenEndpoint;

namespace ScuffedAuth.Tests
{
    public class TokenResponseAssert
    {
        private readonly TokenResponse _tokenResponse;

        public TokenResponseAssert(TokenResponse tokenResponse)
        {
            _tokenResponse = tokenResponse;
        }

        public TokenResponseAssert BeSuccess()
        {
            return HasSuccessStatus()
                .HasEmptyErrorMessage()
                .HasTokenValue();
        }

        public TokenResponseAssert BeFailure()
        {
            return HasFailureStatus()
                .HasErrorMessage()
                .HasEmptyTokenValue();
        }

        public TokenResponseAssert HasSuccessStatus()
        {
            _tokenResponse.Success.Should().BeTrue();
            return this;
        }

        public TokenResponseAssert HasEmptyErrorMessage()
        {
            _tokenResponse.Message.Should().BeEmpty();
            return this;
        }

        public TokenResponseAssert HasTokenValue()
        {
            _tokenResponse.Token.Value.Should().NotBeEmpty();
            return this;
        }

        public TokenResponseAssert HasFailureStatus()
        {
            _tokenResponse.Success.Should().BeFalse();
            return this;
        }

        public TokenResponseAssert HasErrorMessage()
        {
            _tokenResponse.Message.Should().NotBeEmpty();
            return this;
        }

        public TokenResponseAssert HasEmptyTokenValue()
        {
            _tokenResponse.Token.Value.Should().BeEmpty();
            return this;
        }
    }
}
