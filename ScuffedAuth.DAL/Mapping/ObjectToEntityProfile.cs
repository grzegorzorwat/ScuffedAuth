using AutoMapper;
using ScuffedAuth.DAL.Entities;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;

namespace ScuffedAuth.DAL.Mapping
{
    public class ObjectToEntityProfile : Profile
    {
        public ObjectToEntityProfile()
        {
            CreateMap<AuthorizationEndpoint.AuthorizationCode, AuthorizationCodeEntity>()
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => src.ExpiresIn.TotalSeconds)); ;
        }
    }
}
