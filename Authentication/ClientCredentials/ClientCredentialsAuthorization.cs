using Authorization;
using Authorization.TokenEndpoint;
using System.Threading.Tasks;

namespace Authentication.ClientCredentials
{
    internal class ClientCredentialsAuthorization : IAuthorization
    {
        private readonly IClientCredentialsAuthenticator _authenticator;
        private readonly ClientCredentialsDecoder _decoder;

        public ClientCredentialsAuthorization(IClientCredentialsAuthenticator authenticator,
            ClientCredentialsDecoder decoder)
        {
            _authenticator = authenticator;
            _decoder = decoder;
        }

        public async Task<bool> Authorize(string authorizationHeader)
        {
            if (!_decoder.TryDecode(authorizationHeader, out var credentials))
            {
                return false;
            }

            return await _authenticator.Authenticate(credentials.Id, credentials.Secret);
        }
    }
}
