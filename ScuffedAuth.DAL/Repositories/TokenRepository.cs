using Authorization.TokenEndpoint;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;
using System.Threading.Tasks;

namespace ScuffedAuth.DAL.Repositories
{
    internal class TokenRepository : BaseRepository, ITokenRepository
    {
        private readonly IMapper<TokenEntity, Token> _tokenMapper;
        private readonly IMapper<Token, TokenEntity> _tokenEntityMapper;

        public TokenRepository(AppDbContext context,
            IMapper<TokenEntity, Token> tokenMapper,
            IMapper<Token, TokenEntity> tokenEntityMapper) : base(context)
        {
            _tokenMapper = tokenMapper;
            _tokenEntityMapper = tokenEntityMapper;
        }

        public async Task<Token> GetToken(string token)
        {
            var entity = await _context.Tokens.FindAsync(token);
            return _tokenMapper.Map(entity);
        }

        public async Task AddToken(Token token)
        {
            var entity = _tokenEntityMapper.Map(token);
            await _context.Tokens.AddAsync(entity);
        }
    }
}
