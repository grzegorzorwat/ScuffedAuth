using System;

namespace BaseLibrary.Responses
{
    public class RedirectResponse : Response
    {
        public RedirectResponse(string redirectUrl)
        {
            if (string.IsNullOrEmpty(redirectUrl))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(redirectUrl));
            }

            RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; }

        public override T Accept<T>(IResponseVisitor<T> visitor)
        {
            return visitor.VisitRedirectResponse(this);
        }
    }
}
