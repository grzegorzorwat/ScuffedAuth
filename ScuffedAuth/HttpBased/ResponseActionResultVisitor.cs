using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ScuffedAuth.HttpBased
{
    public class ResponseActionResultVisitor : IResponseVisitor<IActionResult>
    {
        public IActionResult VisitRedirectResponse(RedirectResponse response)
        {
            return new RedirectResult(response.RedirectUrl);
        }
    }
}
