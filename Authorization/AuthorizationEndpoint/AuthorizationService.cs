using Authentication.ClientCredentials;
using System.Threading.Tasks;

namespace Authorization.AuthorizationEndpoint
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IAuthorizationCodeGenerator _authorizationCodeGenerator;

        public AuthorizationService(IClientsRepository clientsRepository,
            IAuthorizationCodeGenerator authorizationCodeGenerator)
        {
            _clientsRepository = clientsRepository;
            _authorizationCodeGenerator = authorizationCodeGenerator;
        }

        public async Task<AuthorizationResponse> Authorize(AuthorizationRequest request)
        {
            if(request.ResponseType != "code")
            {
                return new AuthorizationResponse("unsupported_response_type");
            }

            var client = await _clientsRepository.GetClientByIdAsync(request.ClientId);

            if(client is null)
            {
                return new AuthorizationResponse("unauthorized_client");
            }

            return new AuthorizationResponse(_authorizationCodeGenerator.Generate(client));
        }
    }
}
