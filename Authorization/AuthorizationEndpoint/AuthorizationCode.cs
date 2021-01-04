using System;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCode
    {
        public AuthorizationCode(string code, string clientId, DateTime creationDate, int expiresIn)
        {
            Code = code;
            ClientId = clientId;
            CreationDate = creationDate;
            ExpiresIn = expiresIn;
        }

        public string Code { get; init; }

        public string ClientId { get; init; }

        public DateTime CreationDate { get; init; }

        public int ExpiresIn { get; init; }
    }
}
