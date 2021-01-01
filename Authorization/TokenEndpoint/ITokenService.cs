using System.Threading.Tasks;

namespace Authorization.TokenEndpoint
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(TokenRequest request);
    }
}
