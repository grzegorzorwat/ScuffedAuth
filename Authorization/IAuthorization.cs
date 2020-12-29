using Authorization.TokenEndpoint;
using System.Threading.Tasks;

namespace Authorization
{
    public interface IAuthorization
    {
        Task<TokenResponse> GetToken(string authorizationHeader);
    }
}
