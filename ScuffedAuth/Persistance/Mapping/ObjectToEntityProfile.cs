using Authentication.ClientCredentials;
using Authorization.TokenEndpoint;
using AutoMapper;
using ScuffedAuth.Persistance.Entities;

namespace ScuffedAuth.Persistance.Mapping
{
    public class ObjectToEntityProfile : Profile
    {
        public ObjectToEntityProfile()
        {
            CreateMap<Client, ClientEntity>();
            CreateMap<Token, TokenEntity>();
        }
    }
}
