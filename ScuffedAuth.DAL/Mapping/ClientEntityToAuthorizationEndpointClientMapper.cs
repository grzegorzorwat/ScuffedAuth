using Authorization.AuthorizationEndpoint;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL.Mapping
{
    internal class ClientEntityToAuthorizationEndpointClientMapper : IMapper<ClientEntity, Client>
    {
        public Client Map(ClientEntity source)
        {
            return new Client(source.Id);
        }
    }
}
