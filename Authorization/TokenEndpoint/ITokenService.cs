using BaseLibrary.Responses;
using System.Threading.Tasks;

namespace Authorization.TokenEndpoint
{
    public interface ITokenService
    {
        Task<Response> GetToken(TokenRequest request);
    }
}
