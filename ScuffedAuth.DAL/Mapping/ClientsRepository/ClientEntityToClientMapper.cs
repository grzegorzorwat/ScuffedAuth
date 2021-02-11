using Authentication.ClientCredentials;
using ScuffedAuth.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping.ClientsRepository
{
    internal class ClientEntityToClientMapper : IExpressionMapper<ClientEntity, Client>
    {
        public Expression<Func<ClientEntity, Client>> MappingExpression
        {
            get
            {
                return (source) => new Client(source.Id, source.Secret);
            }
        }
    }
}
