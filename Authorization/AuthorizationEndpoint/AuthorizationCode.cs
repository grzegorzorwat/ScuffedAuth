using System;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCode
    {
        public AuthorizationCode(string code, string clientId, DateTime creationDate, int expiresIn, string redirectionUri)
        {
            Code = code;
            ClientId = clientId;
            CreationDate = creationDate;
            ExpiresIn = expiresIn;
            RedirectUri = redirectionUri;
        }

        public string Code { get; init; }

        public string ClientId { get; init; }

        public DateTime CreationDate { get; init; }

        public int ExpiresIn { get; init; }

        public string RedirectUri { get; init; }
    }
}
