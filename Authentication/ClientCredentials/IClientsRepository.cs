using System.Threading.Tasks;

namespace Authentication.ClientCredentials
{
    public interface IClientsRepository
    {
        Task<Client> GetClientByIdAsync(string id);
    }
}
