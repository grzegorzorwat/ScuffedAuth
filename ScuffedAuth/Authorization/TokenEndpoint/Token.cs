namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class Token
    {
        public Token(string value)
        {
            Value = value;
        }

        public string Value { get; init; }

        public static Token Empty => new Token(string.Empty);
    }
}
