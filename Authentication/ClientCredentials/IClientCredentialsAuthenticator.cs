using System.Threading.Tasks;

namespace Authentication.ClientCredentials
{
    public interface IClientCredentialsAuthenticator
    {
        Task<bool> Authenticate(string clientId, string clientSecret);
    }
}
