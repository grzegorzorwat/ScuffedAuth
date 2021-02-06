namespace BaseLibrary.Responses
{
    public class ErrorResponse<PayloadType> : Response
    {
        public ErrorResponse(PayloadType payload)
        {
            Payload = payload;
        }

        public PayloadType Payload { get; }

        public override ReturnType Accept<ReturnType>(IResponseVisitor<ReturnType> visitor)
        {
            return visitor.VisitErrorResponse(this);
        }
    }
}
