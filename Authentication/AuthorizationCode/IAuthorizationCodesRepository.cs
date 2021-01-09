using System.Threading.Tasks;

namespace Authentication.AuthorizationCode
{
    public interface IAuthorizationCodesRepository
    {
        Task<AuthorizationCode?> GetAuthorizationCode(string code);
    }
}