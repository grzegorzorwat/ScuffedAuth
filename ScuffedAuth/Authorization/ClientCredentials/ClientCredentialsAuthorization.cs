using System;
using System.Text;
using ScuffedAuth.Authorization.TokenEndpoint;

namespace ScuffedAuth.Authorization.ClientCredentials
{
    public class ClientCredentialsAuthorization : IAuthorization
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IClientCredentialsAuthenticator _authenticator;
        private readonly ClientCredentialsDecoder _decoder;

        public ClientCredentialsAuthorization(IClientCredentialsAuthenticator authenticator,
            ITokenGenerator tokenGenerator,
            ClientCredentialsDecoder decoder)
        {
            _authenticator = authenticator;
            _tokenGenerator = tokenGenerator;
            _decoder = decoder;
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
            if (!_decoder.TryDecode(authorizationHeader, out var credentials))
            {
                return false;
            }

            return _authenticator.Authenticate(credentials.clientId, credentials.clientSecret);
        }
    }
}
