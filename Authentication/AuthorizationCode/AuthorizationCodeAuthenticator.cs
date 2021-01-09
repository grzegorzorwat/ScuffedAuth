using System.Threading.Tasks;

namespace Authentication.AuthorizationCode
{
    internal class AuthorizationCodeAuthenticator : IAuthenticator
    {
        private readonly IAuthorizationCodesRepository _authrozationCodesRepository;

        public AuthorizationCodeAuthenticator(IAuthorizationCodesRepository authrozationCodesRepository)
        {
            _authrozationCodesRepository = authrozationCodesRepository;
        }

        public async Task<AuthenticationResponse> Authenticate(string authorizationHeader, string query)
        {
            var request = AuthorizationCodeRequest.ParseQuery(query);

            if (request.IsValid)
            {
                var authorizationCode = await _authrozationCodesRepository.GetAuthorizationCode(request.Code);

                if (authorizationCode is not null
                    && authorizationCode.ClientId == request.ClientId
                    && !authorizationCode.IsExpired)
                {
                    return new AuthenticationResponse(new ResponseClient(authorizationCode.ClientId));
                }
            }

            return new AuthenticationResponse("invalid request");
        }
    }
}
