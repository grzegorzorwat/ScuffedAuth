using Authorization.AuthorizationEndpoint;
using ScuffedAuth.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping
{
    internal class AuthorizationEnpointClientToClientEntity : IExpressionMapper<Client, ClientEntity>
    {
        public Expression<Func<Client, ClientEntity>> MappingExpression
        {
            get
            {
                return (source) => new ClientEntity()
                {
                    Id = source.Id,
                    RedirectUri = source.RedirectUri
                };
            }
        }
    }
}
