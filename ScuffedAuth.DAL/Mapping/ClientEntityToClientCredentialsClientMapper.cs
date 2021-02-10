﻿using Authentication.ClientCredentials;
using ScuffedAuth.DAL.Entities;
using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping
{
    internal class ClientEntityToClientCredentialsClientMapper : IExpressionMapper<ClientEntity, Client>
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
