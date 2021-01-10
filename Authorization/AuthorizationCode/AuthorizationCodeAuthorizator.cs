using System.Threading.Tasks;

namespace Authorization.AuthorizationCode
{
    internal class AuthorizationCodeAuthorizator : IAuthorizator
    {
        private readonly IAuthorizationCodesRepository _authrozationCodesRepository;
        private readonly AuthorizationRequest _request;

        public AuthorizationCodeAuthorizator(IAuthorizationCodesRepository authrozationCodesRepository, AuthorizationRequest request)
        {
            _authrozationCodesRepository = authrozationCodesRepository;
            _request = request;
        }

        public async Task<bool> Authorize()
        {
            if (!string.IsNullOrEmpty(_request.Code)
                && !string.IsNullOrEmpty(_request.ClientId))
            {
                var authorizationCode = await _authrozationCodesRepository.GetAuthorizationCode(_request.Code);

                if (authorizationCode is not null
                    && authorizationCode.ClientId == _request.ClientId
                    && !authorizationCode.IsExpired)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
