namespace BaseLibrary.Responses
{
    public interface IResponseVisitor<T>
    {
        T VisitRedirectResponse(RedirectResponse response);
    }
}
