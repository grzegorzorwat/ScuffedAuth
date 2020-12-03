using ScuffedAuth.Authorization.TokenEndpoint;
using System.Threading.Tasks;

namespace ScuffedAuth.Authorization
{
    public class UnidentifiedAuthorization : IAuthorization
    {
        public async Task<TokenResponse> GetToken(string authorizationHeader)
        {
            return await Task.FromResult(new TokenResponse("Grant type must be defined."));
        }
    }
}
