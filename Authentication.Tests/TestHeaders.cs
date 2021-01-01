using System;
using System.Text;

namespace Authentication.Tests
{
    public class TestHeaders
    {
        public const string ClientId = "clientId";
        public const string ClientSecret = "clientSecret";
        public const string EncodedClientSecret = "1000.39zyePe+fstN7VVEitrNyg==.fDCT8OLtWjHKhotdLb43EJm0jBehkp6J45NGyMvFYAw=";

        public static string GetCorrectClientsCredentialsBasicHeader()
        {
            return CreateBasicHeader(ClientId, ClientSecret);
        }

        public static string CreateBasicHeader(string clientId, string clientSecret)
        {
            string encoded = Encode($"{clientId}:{clientSecret}");
            return $"Basic {encoded}";
        }

        protected static string Encode(string source)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(source));
        }
    }
}
