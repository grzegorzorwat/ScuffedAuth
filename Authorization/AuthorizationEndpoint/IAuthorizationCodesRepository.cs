using System.Threading.Tasks;

namespace Authorization.AuthorizationEndpoint
{
    public interface IAuthorizationCodesRepository
    {
        Task AddAuthorizationCode(AuthorizationCode authorizationCode);
        Task<Client> GetClientByIdAsync(string clientId);
    }
}
