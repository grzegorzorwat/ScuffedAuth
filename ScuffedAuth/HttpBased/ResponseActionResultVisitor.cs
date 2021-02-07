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

        public ActionResult VisitSuccessResponse(SuccessResponse response)
        {
            return new OkResult();
        }

        public ActionResult VisitSuccessResponse<PayloadType>(SuccessResponse<PayloadType> response)
        {
            return new OkObjectResult(response.Payload);
        }
    }
}
