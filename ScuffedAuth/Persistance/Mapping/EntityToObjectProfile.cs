using Authorization.AuthorizationCode;
using Authorization.TokenEndpoint;
using AutoMapper;
using ScuffedAuth.Persistance.Entities;
using System;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;
using ClientCredentials = Authentication.ClientCredentials;

namespace ScuffedAuth.Persistance.Mapping
{
    public class EntityToObjectProfile : Profile
    {
        public EntityToObjectProfile()
        {
            CreateMap<ClientEntity, ClientCredentials.Client>();
            CreateMap<ClientEntity, AuthorizationEndpoint.Client>();
            CreateMap<TokenEntity, Token>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.ExpiresIn)));
            CreateMap<AuthorizationCodeEntity, AuthorizationCode>();
        }
    }
}
