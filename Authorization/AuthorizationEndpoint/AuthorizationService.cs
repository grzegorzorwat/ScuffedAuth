using System;
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

                string redirectionUri = request.RedirectUri
                    ?? client.RedirectionUri
                    ?? string.Empty;

                if (IsInvalidRedirectUri(redirectionUri))
                {
                    return new AuthorizationResponse("invalid_request");
                }

                var authorizationCode = _authorizationCodeGenerator.Generate(client.Id, redirectionUri);
                await _authorizationCodesRepository.AddAuthorizationCode(authorizationCode);
                await _unitOfWork.Complete();
                return new AuthorizationResponse(authorizationCode);
            }
            catch
            {
                return new AuthorizationResponse("An error occurred when getting authorization code.");
            }
        }

        private static bool IsInvalidRedirectUri(string redirectionUri)
        {
            return string.IsNullOrEmpty(redirectionUri)
                || !(Uri.TryCreate(redirectionUri, UriKind.Absolute, out var result)
                    && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps));
        }
    }
}
