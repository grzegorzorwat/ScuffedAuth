using System.Threading.Tasks;

namespace Authentication.ClientCredentials
{
    internal interface IClientCredentialsAuthenticator
    {
        Task<bool> Authenticate(string clientId, string clientSecret);
    }
}
