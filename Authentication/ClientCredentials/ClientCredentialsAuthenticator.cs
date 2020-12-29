using System.Threading.Tasks;

namespace Authentication.ClientCredentials
{
    public class ClientCredentialsAuthenticator : IClientCredentialsAuthenticator
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly ISecretVerifier _secretVerifier;

        public ClientCredentialsAuthenticator(IClientsRepository clientsRepository,
            ISecretVerifier secretVerifier)
        {
            _clientsRepository = clientsRepository;
            _secretVerifier = secretVerifier;
        }

        public async Task<bool> Authenticate(string clientId, string clientSecret)
        {
            var client = await _clientsRepository.GetClientByIdAsync(clientId);

            if (client == null)
            {
                return false;
            }

            return _secretVerifier.Verify(client.Secret, clientSecret);
        }
    }
}
