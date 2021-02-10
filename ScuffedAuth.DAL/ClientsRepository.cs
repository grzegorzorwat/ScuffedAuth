using Authentication.ClientCredentials;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;
using System.Threading.Tasks;

namespace ScuffedAuth.DAL
{
    internal class ClientsRepository : BaseRepository, IClientsRepository
    {
        private readonly IMapper<ClientEntity, Client> _clientMapper;

        public ClientsRepository(AppDbContext context,
            IMapper<ClientEntity, Client> clientMapper) : base(context)
        {
            _clientMapper = clientMapper;
        }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            var clientEntity = await _context.Clients.FindAsync(id);
            return _clientMapper.Map(clientEntity);
        }
    }
}
