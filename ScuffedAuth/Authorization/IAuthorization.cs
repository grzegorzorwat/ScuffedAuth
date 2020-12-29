using ScuffedAuth.Authorization.TokenEndpoint;
using System.Threading.Tasks;

namespace ScuffedAuth.Authorization
{
    public interface IAuthorization
    {
        Task<TokenResponse> GetToken(string authorizationHeader);
    }
}
