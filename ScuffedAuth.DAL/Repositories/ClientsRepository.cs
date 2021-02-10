using Authentication.ClientCredentials;
using Microsoft.EntityFrameworkCore;
using ScuffedAuth.DAL.Entities;
using ScuffedAuth.DAL.Mapping;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedAuth.DAL.Repositories
{
    internal class ClientsRepository : BaseRepository, IClientsRepository
    {
        public ClientsRepository(AppDbContext context,
            IExpressionMappingService mappingService) : base(context, mappingService) { }

        public async Task<Client> GetClientByIdAsync(string id)
        {
            return await _context.Clients
                   .Where(x => x.Id == id)
                   .Select(_mappingService.MappingExpression<ClientEntity, Client>())
                   .FirstOrDefaultAsync();
        }
    }
}
