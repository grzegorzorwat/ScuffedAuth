using Authorization.AuthorizationEndpoint;
using Authorization.IntrospectionEnpoint;
using Authorization.TokenEndpoint;
using AutoMapper;

namespace ScuffedAuth.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Token, TokenResource>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Value));
            CreateMap<TokenInfo, TokenInfoResource>();
            CreateMap<AuthorizationCode, AuthorizationCodeResource>();
        }
    }
}
