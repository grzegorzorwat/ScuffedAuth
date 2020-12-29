using Authentication.ClientCredentials;
using System.Threading.Tasks;

namespace ScuffedAuth.Persistance
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
