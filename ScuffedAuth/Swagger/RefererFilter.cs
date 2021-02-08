using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScuffedAuth.Swagger
{
    internal class RefererFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parametersToRemove = operation.Parameters.Where(x => x.Name == "Referer").ToList();

            foreach (var parameter in parametersToRemove)
            {
                operation.Parameters.Remove(parameter);
            }
        }
    }
}
