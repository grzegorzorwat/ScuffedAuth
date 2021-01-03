using Authentication.ClientCredentials;
using Authorization.TokenEndpoint;
using AutoMapper;
using ScuffedAuth.Persistance.Entities;

namespace ScuffedAuth.Persistance.Mapping
{
    public class EntityToObjectProfile : Profile
    {
        public EntityToObjectProfile()
        {
            CreateMap<ClientEntity, Client>();
            CreateMap<TokenEntity, Token>();
        }
    }
}
