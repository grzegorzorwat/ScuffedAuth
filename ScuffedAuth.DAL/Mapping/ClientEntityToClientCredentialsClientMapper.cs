using Authentication.ClientCredentials;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL.Mapping
{
    internal class ClientEntityToClientCredentialsClientMapper : IMapper<ClientEntity, Client>
    {
        public Client Map(ClientEntity source)
        {
            return new Client(source.Id, source.Secret);
        }
    }
}
