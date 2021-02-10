using Authorization.AuthorizationEndpoint;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL.Mapping
{
    internal class ClientEntityToAuthorizationEndpointClientMapper : IMapper<ClientEntity, Client>
    {
        public Client Map(ClientEntity source)
        {
            if (source == null)
            {
                return null;
            }

            return new Client(source.Id);
        }
    }
}
