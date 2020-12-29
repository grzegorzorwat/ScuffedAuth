using Authorization.TokenEndpoint;
using System.Threading.Tasks;

namespace ScuffedAuth.Persistance
{
    public class TokenRepository : BaseRepository, ITokenRepository
    {
        public TokenRepository(AppDbContext context) : base(context) { }

        public async Task<Token> GetToken(string token)
        {
            return await _context.Tokens.FindAsync(token);
        }

        public async Task AddToken(Token token)
        {
            await _context.Tokens.AddAsync(token);
        }
    }
}
