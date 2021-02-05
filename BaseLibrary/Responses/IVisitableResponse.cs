namespace BaseLibrary.Responses
{
    public interface IVisitableResponse
    {
        T Accept<T>(IResponseVisitor<T> visitor);
    }
}
