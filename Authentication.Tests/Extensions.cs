using BaseLibrary.Responses;
using FluentAssertions;

namespace Authentication.Tests
{
    public static class Extensions
    {
        public static void ShouldBeFailure(this Response response, string because = "")
        {
            response.Should().BeOfType<ErrorResponse<string>>(because);
            response.As<ErrorResponse<string>>().Payload.Should().NotBeEmpty(because);
        }

        public static void ShouldBeSuccess(this Response response, string because = "")
        {
            response.Should().BeOfType<SuccessResponse<ResponseClient>>(because);
            response.As<SuccessResponse<ResponseClient>>().Payload.Should().NotBeNull(because);
        }

        public static T As<T>(this Response response) where T : Response
        {
            return response as T;
        }
    }
}
