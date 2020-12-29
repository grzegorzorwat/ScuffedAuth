using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.ClientCredentials
{
    public interface IClientsRepository
    {
        Task<Client> GetClientByIdAsync(string id);
    }
}
