using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace Authentication.AuthorizationCode
{
    internal class AuthorizationCodeRequest
    {
        private const string CodeComponentKey = "code";
        private const string ClientIdComponentKey = "client_id";

        public AuthorizationCodeRequest(string code, string clientId)
        {
            Code = code;
            ClientId = clientId;
        }

        public string Code { get; }

        public string ClientId { get; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Code)
                    && !string.IsNullOrEmpty(ClientId);
            }
        }

        public static AuthorizationCodeRequest ParseQuery(string query)
        {
            var components = QueryHelpers.ParseQuery(query);
            string code = GetValue(components, CodeComponentKey);
            string clientId = GetValue(components, ClientIdComponentKey);
            return new AuthorizationCodeRequest(code, clientId);
        }

        private static string GetValue(Dictionary<string, StringValues> components, string key)
        {
            string value = string.Empty;

            if (components.ContainsKey(key)
                && components[key].Count == 1)
            {
                value = components[key];
            }

            return value;
        }
    }
}
