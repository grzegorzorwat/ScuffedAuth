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
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => src.ExpiresIn.TotalSeconds));
            CreateMap<TokenInfo, TokenInfoResource>();
            CreateMap<AuthorizationCode, AuthorizationCodeResource>();
        }
    }
}
