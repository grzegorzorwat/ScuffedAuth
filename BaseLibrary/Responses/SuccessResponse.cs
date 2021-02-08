using System;

namespace BaseLibrary.Responses
{
    public class SuccessResponse : Response
    {
        public override T Accept<T>(IResponseVisitor<T> visitor)
        {
            return visitor.VisitSuccessResponse(this);
        }
    }

    public class SuccessResponse<PayloadType> : Response
    {
        public SuccessResponse(PayloadType payload)
        {
            Payload = payload;
        }

        public PayloadType Payload { get; }

        public override T Accept<T>(IResponseVisitor<T> visitor)
        {
            return visitor.VisitSuccessResponse<PayloadType>(this);
        }
    }
}
