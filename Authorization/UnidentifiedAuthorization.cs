using Authorization.TokenEndpoint;
using System.Threading.Tasks;

namespace Authorization
{
    public class UnidentifiedAuthorization : IAuthorization
    {
        public async Task<bool> Authorize(string authorizationHeader)
        {
            return await Task.FromResult(false);
        }
    }
}
