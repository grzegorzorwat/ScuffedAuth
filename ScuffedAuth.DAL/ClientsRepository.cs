using Authentication.ClientCredentials;
using AutoMapper;
using ScuffedAuth.DAL.Entities;
using System.Threading.Tasks;

namespace ScuffedAuth.DAL
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
