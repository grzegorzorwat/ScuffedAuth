using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string authorizationHeader, TokenRequest request);
    }
}
