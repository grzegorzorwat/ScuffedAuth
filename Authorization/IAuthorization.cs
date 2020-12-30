using Authorization.TokenEndpoint;
using System.Threading.Tasks;

namespace Authorization
{
    public interface IAuthorization
    {
        Task<bool> Authorize(string authorizationHeader);
    }
}
