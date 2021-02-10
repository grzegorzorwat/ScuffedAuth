using Authorization.TokenEndpoint;
using ScuffedAuth.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping
{
    internal class TokenToTokenEntity : IExpressionMapper<Token, TokenEntity>
    {
        public Expression<Func<Token, TokenEntity>> MappingExpression
        {
            get
            {
                return (source) => new TokenEntity()
                {
                    Value = source.Code,
                    CreationDate = source.CreationDate,
                    ExpiresIn = (int)source.ExpiresIn.TotalSeconds,
                    TokenType = source.TokenType
                };
            }
        }
    }
}
