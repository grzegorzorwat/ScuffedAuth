using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping
{
    internal interface IExpressionMapper<TSource, TDestination>
    {
        Expression<Func<TSource, TDestination>> MappingExpression { get; }
    }
}
