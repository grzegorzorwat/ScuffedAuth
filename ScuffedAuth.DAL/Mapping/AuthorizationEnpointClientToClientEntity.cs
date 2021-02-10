using Authorization.AuthorizationEndpoint;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL.Mapping
{
    internal class AuthorizationEnpointClientToClientEntity : IMapper<Client, ClientEntity>
    {
        public ClientEntity Map(Client source)
        {
            if (source == null)
            {
                return null;
            }

            return new ClientEntity()
            {
                Id = source.Id,
                RedirectUri = source.RedirectUri
            };
        }
    }
}
