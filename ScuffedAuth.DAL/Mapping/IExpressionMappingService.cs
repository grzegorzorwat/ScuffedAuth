using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping
{
    internal interface IExpressionMappingService
    {
        Expression<Func<TSource, TDestination>> MappingExpression<TSource, TDestination>();
    }
}
