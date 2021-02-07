namespace BaseLibrary.Responses
{
    public interface IResponseVisitor<T>
    {
        T VisitRedirectResponse(RedirectResponse response);

        T VisitErrorResponse<PayloadType>(ErrorResponse<PayloadType> response);

        T VisitSuccessResponse(SuccessResponse response);

        T VisitSuccessResponse<PayloadType>(SuccessResponse<PayloadType> response);
    }
}
