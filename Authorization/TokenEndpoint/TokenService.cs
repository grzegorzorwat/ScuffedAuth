using BaseLibrary;
using BaseLibrary.Responses;
using System.Threading.Tasks;

namespace Authorization.TokenEndpoint
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper<Token, TokenResource> _mapper;

        public TokenService(ITokenRepository tokenRepository,
            IUnitOfWork unitOfWork,
            ITokenGenerator tokenGenerator,
            IMapper<Token, TokenResource> mapper)
        {
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        public async Task<Response> GetToken(TokenRequest request)
        {
            try
            {
                var token = _tokenGenerator.Generate();
                await _tokenRepository.AddToken(token);
                await _unitOfWork.Complete();
                var resource = _mapper.Map(token);
                return new SuccessResponse<TokenResource>(resource);
            }
            catch
            {
                return new ErrorResponse("An error occurred when saving the token.");
            }
        }
    }
}
