using Authorization.TokenEndpoint;
using AutoMapper;
using ScuffedAuth.Persistance.Entities;
using System.Threading.Tasks;

namespace ScuffedAuth.Persistance
{
    public class TokenRepository : BaseRepository, ITokenRepository
    {
        public TokenRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<Token> GetToken(string token)
        {
            var entity = await _context.Tokens.FindAsync(token);
            return _mapper.Map<TokenEntity, Token>(entity);
        }

        public async Task AddToken(Token token)
        {
            var entity = _mapper.Map<Token, TokenEntity>(token);
            await _context.Tokens.AddAsync(entity);
        }
    }
}
