using System;
using System.Text;
using ScuffedAuth.Authorization.TokenEndpoint;

namespace ScuffedAuth.Authorization.ClientCredentials
{
    public class ClientCredentialsAuthorization : IAuthorization
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IClientCredentialsAuthenticator _authenticator;

        public ClientCredentialsAuthorization(IClientCredentialsAuthenticator authenticator, ITokenGenerator tokenGenerator)
        {
            _authenticator = authenticator;
            _tokenGenerator = tokenGenerator;
        }

        public TokenResponse GetToken(string authorizationHeader)
        {
            if (!Authorize(authorizationHeader))
            {
                return new TokenResponse("Invalid credentials");
            }

            return new TokenResponse(_tokenGenerator.Generate());
        }

        private bool Authorize(string authorizationHeader)
        {
            var (clientId, clientSecret) = DecodeCredentials(authorizationHeader);

            if (string.IsNullOrEmpty(clientId)
                || string.IsNullOrEmpty(clientSecret))
            {
                return false;
            }

            return _authenticator.Authenticate(clientId, clientSecret);
        }

        private (string clientId, string clientSecret) DecodeCredentials(string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader) ||
                !authorizationHeader.StartsWith("Basic "))
            {
                return (string.Empty, string.Empty);
            }

            string encoded = authorizationHeader.Replace("Basic ", string.Empty);
            var buffer = new Span<byte>(new byte[encoded.Length]);

            if (!Convert.TryFromBase64String(encoded, buffer, out int bytesParsed))
            {
                return (string.Empty, string.Empty);
            }

            string decoded = Encoding.ASCII.GetString(buffer.Slice(0, bytesParsed));
            int separatorIndex = decoded.IndexOf(':');

            if (separatorIndex == -1)
            {
                return (string.Empty, string.Empty);
            }

            string clientId = decoded[..separatorIndex];
            string clientSecret = decoded[(separatorIndex + 1)..];
            return (clientId, clientSecret);
        }
    }
}
