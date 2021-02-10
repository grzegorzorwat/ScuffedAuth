using Authorization.TokenEndpoint;
using Microsoft.EntityFrameworkCore;
using ScuffedAuth.DAL.Entities;
using ScuffedAuth.DAL.Mapping;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedAuth.DAL.Repositories
{
    internal class TokenRepository : BaseRepository, ITokenRepository
    {
        public TokenRepository(AppDbContext context,
            IExpressionMappingService mappingService) : base(context, mappingService) { }

        public async Task<Token> GetToken(string token)
        {
            return await _context.Tokens
                .Where(x => x.Value == token)
                .Select(_mappingService.MappingExpression<TokenEntity, Token>())
                .FirstOrDefaultAsync();
        }

        public async Task AddToken(Token token)
        {
            var entity = _mappingService.MappingExpression<Token, TokenEntity>().Compile().Invoke(token);
            await _context.Tokens.AddAsync(entity);
        }
    }
}
