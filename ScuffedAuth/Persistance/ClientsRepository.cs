using Authentication.ClientCredentials;
using AutoMapper;
using ScuffedAuth.Persistance.Entities;
using System.Threading.Tasks;

namespace ScuffedAuth.Persistance
{
    public class ClientsRepository : BaseRepository, IClientsRepository
    {
        public ClientsRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            var clientEntity = await _context.Clients.FindAsync(id);
            return _mapper.Map<ClientEntity, Client>(clientEntity);
        }
    }
}
