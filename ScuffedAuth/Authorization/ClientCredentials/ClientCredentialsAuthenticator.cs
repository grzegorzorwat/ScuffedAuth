using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.ClientCredentials
{
    public class ClientCredentialsAuthenticator : IClientCredentialsAuthenticator
    {
        private readonly IClientsRepository _clientsRepository;

        public ClientCredentialsAuthenticator(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public async Task<bool> Authenticate(string clientId, string clientSecret)
        {
            var client = await _clientsRepository.GetClientByIdAsync(clientId);

            if(client == null)
            {
                return false;
            }

            return clientSecret == client.Secret;
        }
    }
}
