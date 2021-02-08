namespace BaseLibrary.Responses
{
    public abstract class Response : IVisitableResponse
    {
        public abstract T Accept<T>(IResponseVisitor<T> visitor);
    }
}
