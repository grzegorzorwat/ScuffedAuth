using Authentication.ClientCredentials;
using System;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCode
    {
        public AuthorizationCode(string code, Client client, DateTime creationDate, int expiresIn)
        {
            Code = code;
            Client = client;
            CreationDate = creationDate;
            ExpiresIn = expiresIn;
        }

        public string Code { get; init; }

        public Client Client { get; init; }

        public DateTime CreationDate { get; init; }

        public int ExpiresIn { get; init; }
    }
}
