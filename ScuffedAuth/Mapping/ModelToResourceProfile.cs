using AutoMapper;
using ScuffedAuth.Authorization.TokenEndpoint;

namespace ScuffedAuth.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Token, TokenResource>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Value));
        }
    }
}
