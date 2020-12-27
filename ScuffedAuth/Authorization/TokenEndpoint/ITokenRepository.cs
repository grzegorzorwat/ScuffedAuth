using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public interface ITokenRepository
    {
        Task AddToken(Token token);
        Task<Token> GetToken(string token);
    }
}
