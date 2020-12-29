namespace Authorization.IntrospectionEnpoint
{
    public class TokenInfo
    {
        public TokenInfo(string token, bool active)
        {
            Token = token;
            Active = active;
        }

        public string Token { get; set; }

        public bool Active { get; set; }

        public static TokenInfo Empty => new TokenInfo(string.Empty, false);
    }
}