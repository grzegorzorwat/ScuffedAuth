using Authorization.TokenEndpoint;
using AutoMapper;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;
using System.Threading.Tasks;

namespace ScuffedAuth.DAL
{
    internal class TokenRepository : BaseRepository, ITokenRepository
    {
        private readonly IMapper<TokenEntity, Token> _tokenMapper;

        public TokenRepository(AppDbContext context,
            IMapper mapper,
            IMapper<TokenEntity, Token> tokenMapper) : base(context, mapper)
        {
            _tokenMapper = tokenMapper;
        }

        public async Task<Token> GetToken(string token)
        {
            var entity = await _context.Tokens.FindAsync(token);
            return _tokenMapper.Map(entity);
        }

        public async Task AddToken(Token token)
        {
            var entity = _mapper.Map<Token, TokenEntity>(token);
            await _context.Tokens.AddAsync(entity);
        }
    }
}
