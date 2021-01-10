using System.Threading.Tasks;

namespace Authentication
{
    internal class PassThroughAuthenticator : IAuthenticator
    {
        public Task<AuthenticationResponse> Authenticate(string authorizationHeader)
        {
            return Task.FromResult(new AuthenticationResponse(new ResponseClient(string.Empty)));
        }
    }
}
