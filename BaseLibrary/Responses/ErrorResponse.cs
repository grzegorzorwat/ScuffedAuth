namespace BaseLibrary.Responses
{
    public class ErrorResponse : Response
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public override T Accept<T>(IResponseVisitor<T> visitor)
        {
            return visitor.VisitErrorResponse(this);
        }
    }

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
