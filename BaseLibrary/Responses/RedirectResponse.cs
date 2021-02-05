namespace BaseLibrary.Responses
{
    public class RedirectResponse : Response
    {
        public RedirectResponse(string redirectUrl)
        {
            RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; set; }

        public override T Accept<T>(IResponseVisitor<T> visitor)
        {
            return visitor.VisitRedirectResponse(this);
        }
    }
}
