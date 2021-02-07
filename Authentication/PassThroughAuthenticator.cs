using BaseLibrary.Responses;
using System.Threading.Tasks;

namespace Authentication
{
    internal class PassThroughAuthenticator : IAuthenticator
    {
        public Task<Response> Authenticate(string authorizationHeader)
        {
            return Task.FromResult((Response)new SuccessResponse());
        }
    }
}
