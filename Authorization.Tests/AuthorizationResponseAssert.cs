using Authorization.AuthorizationEndpoint;
using FluentAssertions;
using Tests.Library;

namespace Authorization.Tests
{
    public class AuthorizationResponseAssert : BaseResponseAssert
    {
        private readonly AuthorizationResponse _response;

        public AuthorizationResponseAssert(AuthorizationResponse response) : base(response)
        {
            _response = response;
        }

        public override BaseResponseAssert HasPayloadForFailureResponse(string because = "")
        {
            return HasEmptyAuthorizationCode(because);
        }

        public AuthorizationResponseAssert HasEmptyAuthorizationCode(string because = "")
        {
            _response.AuthorizationCode.Should().BeNull(because);
            return this;
        }

        public override BaseResponseAssert HasPayloadForSuccessReponse(string because = "")
        {
            return HasAuthorizationCode(because);
        }

        public AuthorizationResponseAssert HasAuthorizationCode(string because = "")
        {
            _response.AuthorizationCode.Should().NotBeNull(because);
            _response.AuthorizationCode.Code.Should().NotBeEmpty(because);
            return this;
        }
    }
}
