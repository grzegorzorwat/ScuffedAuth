using BaseLibrary.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ScuffedAuth.HttpBased
{
    public class ResponseActionResultVisitor : IResponseVisitor<ActionResult>
    {
        public ActionResult VisitErrorResponse<PayloadType>(ErrorResponse<PayloadType> response)
        {
            return new BadRequestObjectResult(response.Payload);
        }

        public ActionResult VisitRedirectResponse(RedirectResponse response)
        {
            return new RedirectResult(response.RedirectUrl);
        }
    }
}
