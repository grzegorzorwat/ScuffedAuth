using Authentication;
using Authentication.ClientCredentials;
using Authorization;
using Authorization.IntrospectionEnpoint;
using Authorization.TokenEndpoint;
using AutoMapper;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScuffedAuth.HttpBased;
using ScuffedAuth.Middlewares.Authentication;
using ScuffedAuth.Middlewares.Authorization;
using ScuffedAuth.Persistance;
using System;
using AuthorizationCode = Authorization.AuthorizationCode;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;

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

                    c.OperationFilter<Swagger.RefererFilter>();
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
            services
                .AddOptions<Authorization.Codes.ExpiringCodesGeneratorSettings> ()
                .Bind(Configuration.GetSection("AuthorizationCodeSettings"))
                .ValidateDataAnnotations();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IClientsRepository, ClientsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IIntrospectionService, IntrospectionService>();
            services.AddScoped<AuthorizationEndpoint.IAuthorizationService, AuthorizationEndpoint.AuthorizationService>();
            services.AddScoped<AuthorizationEndpoint.IAuthorizationCodesRepository, AuthorizationCodesRepository>();
            services.AddScoped<AuthorizationEndpoint.IAuthorizationCodeGenerator, AuthorizationEndpoint.AuthorizationCodeGenerator>();
            services.AddScoped<AuthorizationCode.IAuthorizationCodesRepository, AuthorizationCodesRepository>();

            services.AddAuthenticaticators();
            services.RegisterAuthorization();

            services.AddHttpContextAccessor();
            services
                .AddAuthentication()
                .AddScheme<GrantTypesAuthenticationSchemeOptions, GrantTypesAuthenticationHandler>(
                    AuthenticationSchemeConstants.GrantTypesAuthenticationScheme, op => { });
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("GrantTypeAuthorization", policy =>
                        policy.Requirements.Add(new GrantTypesAuthorizationRequirement()));
                });
            services.AddScoped<IAuthorizationHandler, GrantTypesAuthorizationHandler>();
            services.AddScoped<AuthorizationEndpoint.IAuthorizationCodeAuthentication, AuthorizationCodeAuthentication>();
            services.AddScoped<IResponseVisitor<ActionResult>, ResponseActionResultVisitor>();
            services.AddScoped<IResponseVisitor<AuthenticateResult>, ResponseAuthenticateResultVisitor>();
            services.AddScoped<IClaimsMapper<ResponseClient>, Middlewares.Authentication.ClaimsMapper>();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScuffedAuth v1"));
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
