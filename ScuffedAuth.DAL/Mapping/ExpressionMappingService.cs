using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;

namespace ScuffedAuth.DAL.Mapping
{
    internal class ExpressionMappingService : IExpressionMappingService
    {
        private readonly IServiceProvider serviceProvider;

        public ExpressionMappingService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Expression<Func<TSource, TDestination>> MappingExpression<TSource, TDestination>()
        {
            var mapper = serviceProvider.GetRequiredService<IExpressionMapper<TSource, TDestination>>();
            return mapper.MappingExpression;
        }
    }
}
