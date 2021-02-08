using System;

namespace ScuffedAuth.DAL.Entities
{
    public class AuthorizationCodeEntity
    {
        public string Code { get; set; }

        public string ClientId { get; set; }

        public DateTime CreationDate { get; set; }

        public int ExpiresIn { get; set; }

        public string RedirectUri { get; set; }
    }
}
