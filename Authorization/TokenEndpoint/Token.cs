using System;

namespace Authorization.TokenEndpoint
{
    public class Token
    {
        public Token(string value, string tokenType, DateTime creationDate, int expiresIn)
        {
            Value = value;
            TokenType = tokenType;
            CreationDate = creationDate;
            ExpiresIn = expiresIn;
        }

        public string Value { get; init; }

        public string TokenType { get; init; }

        public DateTime CreationDate { get; init; }

        public int ExpiresIn { get; init; }

        public static Token Empty => new Token(string.Empty, string.Empty, DateTime.UtcNow, 0);
    }
}
