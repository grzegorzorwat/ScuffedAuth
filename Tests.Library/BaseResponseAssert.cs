using Authorization;
using FluentAssertions;

namespace Tests.Library
{
    public abstract class BaseResponseAssert
    {
        private readonly BaseResponse _response;

        public BaseResponseAssert(BaseResponse response)
        {
            _response = response;
        }

        public BaseResponseAssert BeSuccess()
        {
            return HasSuccessStatus()
                .HasEmptyErrorMessage()
                .HasPayloadForSuccessReponse();
        }

        public BaseResponseAssert BeFailure(string because = "")
        {
            return HasFailureStatus(because)
                .HasErrorMessage(because)
                .HasPayloadForFailureResponse();
        }

        public BaseResponseAssert HasSuccessStatus()
        {
            _response.Success.Should().BeTrue();
            return this;
        }

        public BaseResponseAssert HasEmptyErrorMessage()
        {
            _response.Message.Should().BeEmpty();
            return this;
        }

        public BaseResponseAssert HasFailureStatus(string because = "")
        {
            _response.Success.Should().BeFalse(because);
            return this;
        }

        public BaseResponseAssert HasErrorMessage(string because = "")
        {
            _response.Message.Should().NotBeEmpty(because);
            return this;
        }

        public BaseResponseAssert WithMessage(string message, string because = "")
        {
            _response.Message.Should().Be(message, because);
            return this;
        }

        public abstract BaseResponseAssert HasPayloadForSuccessReponse(string because = "");

        public abstract BaseResponseAssert HasPayloadForFailureResponse(string because = "");
    }
}
