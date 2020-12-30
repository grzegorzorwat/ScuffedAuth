using System.Threading.Tasks;

namespace Authorization.TokenEndpoint
{
    public class TokenService : ITokenService
    {
        private readonly IAuthorizationFactory _authorizationFactory;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;

        public TokenService(IAuthorizationFactory authorizationFactory,
            ITokenRepository tokenRepository,
            IUnitOfWork unitOfWork,
            ITokenGenerator tokenGenerator)
        {
            _authorizationFactory = authorizationFactory;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<TokenResponse> GetToken(string authorizationHeader, TokenRequest request)
        {
            try
            {
                var authorization = _authorizationFactory.GetAuthorization(request.GrantType);
                var isAuthorized = await authorization.Authorize(authorizationHeader);

                if (isAuthorized)
                {
                    var token = _tokenGenerator.Generate();
                    await _tokenRepository.AddToken(token);
                    await _unitOfWork.Complete();
                    return new TokenResponse(token);
                }

                return new TokenResponse("Invalid credentials");
            }
            catch
            {
                return new TokenResponse("An error occurred when saving the token.");
            }
        }
    }
}
