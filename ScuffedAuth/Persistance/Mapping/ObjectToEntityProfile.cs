using Authorization.TokenEndpoint;
using AutoMapper;
using ScuffedAuth.Persistance.Entities;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;
using ClientCredentials = Authentication.ClientCredentials;

namespace ScuffedAuth.Persistance.Mapping
{
    public class ObjectToEntityProfile : Profile
    {
        public ObjectToEntityProfile()
        {
            CreateMap<ClientCredentials.Client, ClientEntity>();
            CreateMap<Token, TokenEntity>();
            CreateMap<AuthorizationEndpoint.Client, ClientEntity>();
            CreateMap<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity>()
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => src.ExpiresIn.TotalSeconds)); ;
        }
    }
}
