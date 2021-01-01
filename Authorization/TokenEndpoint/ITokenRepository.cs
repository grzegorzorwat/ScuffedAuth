using System.Threading.Tasks;

namespace Authorization.TokenEndpoint
{
    public interface ITokenRepository
    {
        Task AddToken(Token token);
        Task<Token> GetToken(string token);
    }
}
