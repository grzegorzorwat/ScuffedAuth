using System.Threading.Tasks;

namespace Authorization.AuthorizationCode
{
    public interface IAuthorizationCodesRepository
    {
        Task<AuthorizationCode?> GetAuthorizationCode(string code);
    }
}