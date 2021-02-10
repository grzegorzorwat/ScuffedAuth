using Authorization.AuthorizationCode;
using ScuffedAuth.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping
{
    internal class AuthorizationCodeEntityToAuthorizationCodeMapper : IExpressionMapper<AuthorizationCodeEntity, AuthorizationCode>
    {
        public Expression<Func<AuthorizationCodeEntity, AuthorizationCode>> MappingExpression
        {
            get
            {
                return (source) => new AuthorizationCode()
                {
                    ClientId = source.ClientId,
                    Code = source.Code,
                    CreationDate = source.CreationDate,
                    ExpiresIn = source.ExpiresIn,
                    RedirectUri = source.RedirectUri
                };
            }
        }
    }
}
