﻿using Authorization.TokenEndpoint;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScuffedAuth.DAL.Entities;
using ScuffedAuth.DAL.Mapping;
using ScuffedAuth.DAL.Mapping.AuthorizationCodesRepository;
using ScuffedAuth.DAL.Mapping.TokenRepository;
using ScuffedAuth.DAL.Repositories;
using System;
using AuthorizationCode = Authorization.AuthorizationCode;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;
using ClientCredentials = Authentication.ClientCredentials;

namespace ScuffedAuth.DAL
{
    public static class ServiceConfiguration
    {
        public static void AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<ClientCredentials.IClientsRepository, ClientsRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<AuthorizationEndpoint.IAuthorizationCodesRepository, AuthorizationCodesRepository>();
            services.AddScoped<AuthorizationCode.IAuthorizationCodesRepository, AuthorizationCodesRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IExpressionMapper<ClientEntity, ClientCredentials.Client>,
                Mapping.ClientsRepository.ClientEntityToClientMapper>();
            services.AddScoped<IExpressionMapper<ClientEntity, AuthorizationEndpoint.Client>,
                Mapping.AuthorizationCodesRepository.ClientEntityToClientMapper>();
            services.AddScoped<IExpressionMapper<TokenEntity, Token>,
                TokenEntityToTokenMapper>();
            services.AddScoped<IExpressionMapper<AuthorizationCodeEntity, AuthorizationCode.AuthorizationCode>,
                AuthorizationCodeEntityToAuthorizationCodeMapper>();
            services.AddScoped<IExpressionMapper<Token, TokenEntity>,
                TokenToTokenEntityMapper>();
            services.AddScoped<IExpressionMapper<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity>,
                AuthorizationCodeToAuthorizationCodeEntityMapper>();
            services.AddScoped<IExpressionMappingService, ExpressionMappingService>();
        }

        public static void MigrateScuffedAuth(this IServiceProvider service)
        {
            using var context = service.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
    }
}
