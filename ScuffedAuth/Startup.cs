using Authentication;
using Authentication.ClientCredentials;
using Authorization;
using Authorization.AuthorizationEndpoint;
using Authorization.IntrospectionEnpoint;
using Authorization.TokenEndpoint;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScuffedAuth.Authentication;
using ScuffedAuth.Persistance;
using System;

namespace ScuffedAuth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScuffedAuth", Version = "v1" });
                    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "basic",
                        In = ParameterLocation.Header,
                        Description = "Basic Authorization header using the Bearer scheme."
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                });

            services.AddAutoMapper(typeof(Startup));
            services
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("scuffed-auth-in-memory");
                });
            services
                .AddOptions<TokenGeneratorSettings>()
                .Bind(Configuration.GetSection("TokenGeneratorSettings"))
                .ValidateDataAnnotations();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IIntrospectionService, IntrospectionService>();
            services.AddScoped<Authorization.AuthorizationEndpoint.IAuthorizationService, AuthorizationService>();
            services.AddScoped<IAuthorizationCodesRepository, AuthorizationCodesRepository>();
            services.AddScoped<IAuthorizationCodeGenerator, AuthorizationCodeGenerator>();

            ServicesConfiguration.AddAuthentication(services);
            services.AddHttpContextAccessor();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = AuthenticationSchemeConstants.GrantTypesAuthenticationScheme;
                })
                .AddScheme<GrantTypesAuthenticationSchemeOptions, GrantTypesAuthenticationHandler>(
                    AuthenticationSchemeConstants.GrantTypesAuthenticationScheme, op => { });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScuffedAuth v1"));
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
