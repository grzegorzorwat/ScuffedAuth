using ScuffedAuth.Persistance;
using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.ClientCredentials
{
    public class ClientsRepository : BaseRepository, IClientsRepository
    {
        public ClientsRepository(AppDbContext context) : base(context) { }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            return await _context.Clients.FindAsync(id);
        }
    }
}
