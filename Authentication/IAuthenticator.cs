using System.Threading.Tasks;

namespace Authentication
{
    public interface IAuthenticator
    {
        Task<AuthenticationResponse> Authenticate(string authorizationHeader);
    }
}
