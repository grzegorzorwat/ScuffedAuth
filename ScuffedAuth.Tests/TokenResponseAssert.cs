using Authorization.TokenEndpoint;
using FluentAssertions;

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

        public TokenResponseAssert BeFailure(string because = "")
        {
            return HasFailureStatus(because)
                .HasErrorMessage(because)
                .HasEmptyTokenValue(because);
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

        public TokenResponseAssert HasFailureStatus(string because = "")
        {
            _tokenResponse.Success.Should().BeFalse(because);
            return this;
        }

        public TokenResponseAssert HasErrorMessage(string because = "")
        {
            _tokenResponse.Message.Should().NotBeEmpty(because);
            return this;
        }

        public TokenResponseAssert HasEmptyTokenValue(string because = "")
        {
            _tokenResponse.Token.Value.Should().BeEmpty(because);
            return this;
        }
    }
}
