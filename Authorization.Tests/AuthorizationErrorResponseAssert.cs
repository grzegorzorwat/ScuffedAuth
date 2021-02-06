using Authorization.AuthorizationEndpoint;
using BaseLibrary.Responses;
using FluentAssertions;

namespace Authorization.Tests
{
    public class AuthorizationErrorResponseAssert
    {
        private readonly ErrorResponse<AuthorizationError> _authorizationErrorResponse;

        public AuthorizationErrorResponseAssert(ErrorResponse<AuthorizationError> authorizationErrorResponse)
        {
            _authorizationErrorResponse = authorizationErrorResponse;
        }

        public AuthorizationErrorResponseAssert HaveError(string error, string because = "")
        {
            _authorizationErrorResponse.Payload.Error.Should().Be(error, because);
            return this;
        }
    }
}
