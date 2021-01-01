using System.Threading.Tasks;

namespace Authorization.TokenEndpoint
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;

        public TokenService(ITokenRepository tokenRepository,
            IUnitOfWork unitOfWork,
            ITokenGenerator tokenGenerator)
        {
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<TokenResponse> GetToken(TokenRequest request)
        {
            try
            {
                var token = _tokenGenerator.Generate();
                await _tokenRepository.AddToken(token);
                await _unitOfWork.Complete();
                return new TokenResponse(token);
            }
            catch
            {
                return new TokenResponse("An error occurred when saving the token.");
            }
        }
    }
}
