using BaseLibrary.Responses;
using System.Threading.Tasks;

namespace Authentication
{
    public class UnidentifiedAuthentication : IAuthenticator
    {
        public async Task<Response> Authenticate(string authorizationHeader)
        {
            return await Task.FromResult((Response)new ErrorResponse<string>("Invalid grant type"));
        }
    }
}
