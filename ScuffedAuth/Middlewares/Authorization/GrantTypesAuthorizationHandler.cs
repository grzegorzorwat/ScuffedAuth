using Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ScuffedAuth.Middlewares.Authorization
{
    public class GrantTypesAuthorizationHandler : AuthorizationHandler<GrantTypesAuthorizationRequirement, AuthorizationRequest>
    {
        private readonly AuthorizationFactory _authorizationFactory;

        public GrantTypesAuthorizationHandler(AuthorizationFactory authorizationFactory)
        {
            _authorizationFactory = authorizationFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            GrantTypesAuthorizationRequirement requirement,
            AuthorizationRequest resource)
        {
            var authorizator = _authorizationFactory.GetAuthentication(resource);

            if (await authorizator.Authorize())
            {
                context.Succeed(requirement);
            }
        }
    }
}
