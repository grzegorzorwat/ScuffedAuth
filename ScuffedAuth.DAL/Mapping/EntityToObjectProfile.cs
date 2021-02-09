using Authorization.AuthorizationCode;
using AutoMapper;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL.Mapping
{
    public class EntityToObjectProfile : Profile
    {
        public EntityToObjectProfile()
        {
            CreateMap<AuthorizationCodeEntity, AuthorizationCode>();
        }
    }
}
