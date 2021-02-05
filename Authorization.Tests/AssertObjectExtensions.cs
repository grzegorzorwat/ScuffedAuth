using BaseLibrary.Responses;

namespace Authorization.Tests
{
    public static class AssertObjectExtensions
    {
        public static ResponseAssert Should(this Response response)
        {
            return new ResponseAssert(response);
        }
    }
}
