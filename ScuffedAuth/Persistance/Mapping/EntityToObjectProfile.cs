using Authorization.AuthorizationCode;
using Authorization.TokenEndpoint;
using AutoMapper;
using ScuffedAuth.Persistance.Entities;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;
using ClientCredentials = Authentication.ClientCredentials;

namespace ScuffedAuth.Persistance.Mapping
{
    public class EntityToObjectProfile : Profile
    {
        public EntityToObjectProfile()
        {
            CreateMap<ClientEntity, ClientCredentials.Client>();
            CreateMap<ClientEntity, AuthorizationEndpoint.Client>();
            CreateMap<TokenEntity, Token>();
            CreateMap<AuthorizationCodeEntity, AuthorizationCode>();
        }
    }
}
