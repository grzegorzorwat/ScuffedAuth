using Authorization.AuthorizationEndpoint;
using ScuffedAuth.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping.AuthorizationCodesRepository
{
    internal class AuthorizationCodeToAuthorizationCodeEntityMapper : IExpressionMapper<AuthorizationCode, AuthorizationCodeEntity>
    {
        public Expression<Func<AuthorizationCode, AuthorizationCodeEntity>> MappingExpression
        {
            get
            {
                return (source) => new AuthorizationCodeEntity()
                {
                    ClientId = source.ClientId,
                    Code = source.Code,
                    CreationDate = source.CreationDate,
                    ExpiresIn = (int)source.ExpiresIn.TotalSeconds,
                    RedirectUri = source.RedirectUri
                };
            }
        }
    }
}
