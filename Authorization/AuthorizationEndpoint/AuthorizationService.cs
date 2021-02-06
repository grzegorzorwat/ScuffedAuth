using BaseLibrary.Responses;
using System;
using System.Threading.Tasks;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationCodeGenerator _authorizationCodeGenerator;
        private readonly IAuthorizationCodesRepository _authorizationCodesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorizationCodeAuthentication _authentication;

        public AuthorizationService(IAuthorizationCodeGenerator authorizationCodeGenerator,
            IAuthorizationCodesRepository authorizationCodesRepository,
            IUnitOfWork unitOfWork,
            IAuthorizationCodeAuthentication authentication)
        {
            _authorizationCodeGenerator = authorizationCodeGenerator;
            _authorizationCodesRepository = authorizationCodesRepository;
            _unitOfWork = unitOfWork;
            _authentication = authentication;
        }

        public async Task<Response> Authorize(AuthorizationServiceRequest request)
        {
            string redirectUri = string.Empty;

            try
            {
                var client = await _authorizationCodesRepository.GetClientByIdAsync(request.ClientId);

                if (client is null)
                {
                    return ResponseFactory.WithError("unauthorized_client");
                }

                redirectUri = request.RedirectUri
                    ?? client.RedirectUri
                    ?? string.Empty;

                if (IsInvalidRedirectUri(redirectUri))
                {
                    return ResponseFactory.WithError("invalid_request");
                }

                if (request.ResponseType != ResponseType.code)
                {
                    return ResponseFactory.WithError(redirectUri, "unsupported_response_type");
                }

                var authenticationResponse = _authentication.Authenticate();

                if (authenticationResponse is not null)
                {
                    return authenticationResponse;
                }

                var authorizationCode = _authorizationCodeGenerator.Generate(client.Id, redirectUri);
                await _authorizationCodesRepository.AddAuthorizationCode(authorizationCode);
                await _unitOfWork.Complete();
                return ResponseFactory.With(redirectUri, "code", authorizationCode.Code);
            }
            catch
            {
                return ResponseFactory.WithError(redirectUri, "server_error");
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
