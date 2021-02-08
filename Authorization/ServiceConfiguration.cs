using Microsoft.Extensions.DependencyInjection;

namespace Authorization
{
    public static class ServiceConfiguration
    {
        public static void RegisterAuthorization(this IServiceCollection services)
        {
            services.AddScoped<AuthorizationFactory>();
            services
                .AddScoped<PassThroughAuthorizator>()
                .AddScoped<IAuthorizator, PassThroughAuthorizator>(
                    s => s.GetRequiredService<PassThroughAuthorizator>());
        }
    }
}
