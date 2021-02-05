using BaseLibrary.Responses;

namespace Tests.Library
{
    public class TestResponseVisitor : IResponseVisitor<string>
    {
        public string VisitRedirectResponse(RedirectResponse response)
        {
            return response.RedirectUrl;
        }
    }
}
