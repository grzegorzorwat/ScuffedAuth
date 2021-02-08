﻿using Authorization.TokenEndpoint;
using AutoMapper;
using ScuffedAuth.DAL.Entities;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;
using ClientCredentials = Authentication.ClientCredentials;

namespace ScuffedAuth.DAL.Mapping
{
    public class ObjectToEntityProfile : Profile
    {
        public ObjectToEntityProfile()
        {
            CreateMap<ClientCredentials.Client, ClientEntity>();
            CreateMap<Token, TokenEntity>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => src.ExpiresIn.TotalSeconds));
            CreateMap<AuthorizationEndpoint.Client, ClientEntity>();
            CreateMap<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity>()
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => src.ExpiresIn.TotalSeconds)); ;
        }
    }
}
