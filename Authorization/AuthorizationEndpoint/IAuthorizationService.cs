using System.Threading.Tasks;

namespace Authorization.AuthorizationEndpoint
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResponse> Authorize(AuthorizationRequest request);
    }
}
