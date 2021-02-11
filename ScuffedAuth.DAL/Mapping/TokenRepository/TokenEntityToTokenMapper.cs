using Authorization.TokenEndpoint;
using ScuffedAuth.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping.TokenRepository
{
    internal class TokenEntityToTokenMapper : IExpressionMapper<TokenEntity, Token>
    {
        public Expression<Func<TokenEntity, Token>> MappingExpression
        {
            get
            {
                return (source) => new Token()
                {
                    Code = source.Value,
                    CreationDate = source.CreationDate,
                    ExpiresIn = TimeSpan.FromSeconds(source.ExpiresIn),
                    TokenType = source.TokenType
                };
            }
        }
    }
}
