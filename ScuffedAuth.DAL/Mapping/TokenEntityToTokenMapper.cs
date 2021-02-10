using Authorization.TokenEndpoint;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;
using System;

namespace ScuffedAuth.DAL.Mapping
{
    internal class TokenEntityToTokenMapper : IMapper<TokenEntity, Token>
    {
        public Token Map(TokenEntity source)
        {
            if(source == null)
            {
                return null;
            }

            return new Token()
            {
                Code = source.Value,
                CreationDate = source.CreationDate,
                ExpiresIn = TimeSpan.FromSeconds(source.ExpiresIn),
                TokenType = source.TokenType
            };
        }
    }
}
