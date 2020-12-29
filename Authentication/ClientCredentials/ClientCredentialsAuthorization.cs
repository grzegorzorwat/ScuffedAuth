using Authorization;
using Authorization.TokenEndpoint;
using System.Threading.Tasks;

namespace Authentication.ClientCredentials
{
    internal class ClientCredentialsAuthorization : IAuthorization
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

        public async Task<TokenResponse> GetToken(string authorizationHeader)
        {
            bool isAuthorized = await Authorize(authorizationHeader);

            if (!isAuthorized)
            {
                return new TokenResponse("Invalid credentials");
            }

            return new TokenResponse(_tokenGenerator.Generate());
        }

        private async Task<bool> Authorize(string authorizationHeader)
        {
            if (!_decoder.TryDecode(authorizationHeader, out var credentials))
            {
                return false;
            }

            return await _authenticator.Authenticate(credentials.Id, credentials.Secret);
        }
    }
}
