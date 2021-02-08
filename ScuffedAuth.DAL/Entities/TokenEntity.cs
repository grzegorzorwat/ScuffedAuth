using System;

namespace ScuffedAuth.DAL.Entities
{
    public class TokenEntity
    {
        public string Value { get; set; }

        public string TokenType { get; set; }

        public DateTime CreationDate { get; set; }

        public int ExpiresIn { get; set; }
    }
}
