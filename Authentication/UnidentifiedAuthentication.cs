using System.Threading.Tasks;

namespace Authentication
{
    public class UnidentifiedAuthentication : IAuthenticator
    {
        public async Task<AuthenticationResponse> Authenticate(string authorizationHeader, string query)
        {
            return await Task.FromResult(new AuthenticationResponse("Invalid grant type"));
        }
    }
}
