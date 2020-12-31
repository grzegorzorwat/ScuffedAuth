using Authentication.Tests;
using FluentAssertions;

namespace Authentication.Tests
{
    public class AuthenticationResponseAssert
    {
        private readonly AuthenticationResponse _response;

        public AuthenticationResponseAssert(AuthenticationResponse response)
        {
            _response = response;
        }

        public AuthenticationResponseAssert BeSuccess()
        {
            return HasSuccessStatus()
                .HasEmptyErrorMessage()
                .HasClientId()
                .HasClientSecret();
        }

        public AuthenticationResponseAssert BeFailure(string because = "")
        {
            return HasFailureStatus(because)
                .HasErrorMessage(because)
                .HasEmptyClientId(because)
                .HasEmptyClientSecret();
        }

        public AuthenticationResponseAssert HasSuccessStatus()
        {
            _response.Success.Should().BeTrue();
            return this;
        }

        public AuthenticationResponseAssert HasEmptyErrorMessage()
        {
            _response.Message.Should().BeEmpty();
            return this;
        }

        public AuthenticationResponseAssert HasClientId()
        {
            _response.Client.Id.Should().NotBeEmpty();
            return this;
        }

        public AuthenticationResponseAssert HasClientSecret()
        {
            _response.Client.Secret.Should().NotBeEmpty();
            return this;
        }

        public AuthenticationResponseAssert HasFailureStatus(string because = "")
        {
            _response.Success.Should().BeFalse(because);
            return this;
        }

        public AuthenticationResponseAssert HasErrorMessage(string because = "")
        {
            _response.Message.Should().NotBeEmpty(because);
            return this;
        }

        public AuthenticationResponseAssert HasEmptyClientId(string because = "")
        {
            _response.Client.Id.Should().BeEmpty(because);
            return this;
        }

        public AuthenticationResponseAssert HasEmptyClientSecret(string because = "")
        {
            _response.Client.Secret.Should().BeEmpty(because);
            return this;
        }
    }
}
