using Authorization.TokenEndpoint;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL.Mapping
{
    internal class TokenToTokenEntity : IMapper<Token, TokenEntity>
    {
        public TokenEntity Map(Token source)
        {
            return new TokenEntity()
            {
                Value = source.Code,
                CreationDate = source.CreationDate,
                ExpiresIn = (int)source.ExpiresIn.TotalSeconds,
                TokenType = source.TokenType
            };
        }
    }
}
