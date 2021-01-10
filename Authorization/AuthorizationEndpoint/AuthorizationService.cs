using System.Threading.Tasks;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationCodeGenerator _authorizationCodeGenerator;
        private readonly IAuthorizationCodesRepository _authorizationCodesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizationService(IAuthorizationCodeGenerator authorizationCodeGenerator,
            IAuthorizationCodesRepository authorizationCodesRepository,
            IUnitOfWork unitOfWork)
        {
            _authorizationCodeGenerator = authorizationCodeGenerator;
            _authorizationCodesRepository = authorizationCodesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorizationResponse> Authorize(AuthorizationServiceRequest request)
        {
            try
            {
                if (request.ResponseType != ResponseType.code)
                {
                    return new AuthorizationResponse("unsupported_response_type");
                }

                var client = await _authorizationCodesRepository.GetClientByIdAsync(request.ClientId);

                if (client is null)
                {
                    return new AuthorizationResponse("unauthorized_client");
                }

                var authorizationCode = _authorizationCodeGenerator.Generate(client.Id);
                await _authorizationCodesRepository.AddAuthorizationCode(authorizationCode);
                await _unitOfWork.Complete();
                return new AuthorizationResponse(authorizationCode);
            }
            catch
            {
                return new AuthorizationResponse("An error occurred when saving the token.");
            }
        }
    }
}
