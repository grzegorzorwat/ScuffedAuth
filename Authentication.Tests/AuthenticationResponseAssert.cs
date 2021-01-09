using Authentication.Tests;
using FluentAssertions;
using Tests.Library;

namespace Authentication.Tests
{
    public class AuthenticationResponseAssert : BaseResponseAssert
    {
        private readonly AuthenticationResponse _response;

        public AuthenticationResponseAssert(AuthenticationResponse response) : base(response)
        {
            _response = response;
        }

        public override BaseResponseAssert HasPayloadForSuccessReponse(string because = "")
        {
            return HasClientId(because);
        }

        public override BaseResponseAssert HasPayloadForFailureResponse(string because = "")
        {
            return HasEmptyClientId(because);
        }

        public AuthenticationResponseAssert HasClientId(string because = "")
        {
            _response.Client.Id.Should().NotBeEmpty(because);
            return this;
        }

        public AuthenticationResponseAssert HasEmptyClientId(string because = "")
        {
            _response.Client.Id.Should().BeEmpty(because);
            return this;
        }
    }
}
