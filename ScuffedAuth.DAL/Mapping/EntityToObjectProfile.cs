using Authorization.AuthorizationCode;
using Authorization.TokenEndpoint;
using AutoMapper;
using ScuffedAuth.DAL.Entities;
using System;

namespace ScuffedAuth.DAL.Mapping
{
    public class EntityToObjectProfile : Profile
    {
        public EntityToObjectProfile()
        {
            CreateMap<TokenEntity, Token>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => TimeSpan.FromSeconds(src.ExpiresIn)));
            CreateMap<AuthorizationCodeEntity, AuthorizationCode>();
        }
    }
}
