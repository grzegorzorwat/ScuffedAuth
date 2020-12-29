using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.ClientCredentials
{
    public interface IClientCredentialsAuthenticator
    {
        Task<bool> Authenticate(string clientId, string clientSecret);
    }
}
