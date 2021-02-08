using BaseLibrary.Responses;
using System.Threading.Tasks;

namespace Authentication
{
    public interface IAuthenticator
    {
        Task<Response> Authenticate(string authorizationHeader);
    }
}
