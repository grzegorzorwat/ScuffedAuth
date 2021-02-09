using Authorization.AuthorizationEndpoint;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL.Mapping
{
    internal class AuthorizationCodeToAuthorizationCodeEntityMapper : IMapper<AuthorizationCode, AuthorizationCodeEntity>
    {
        public AuthorizationCodeEntity Map(AuthorizationCode source)
        {
            return new AuthorizationCodeEntity()
            {
                ClientId = source.ClientId,
                Code = source.Code,
                CreationDate = source.CreationDate,
                ExpiresIn = (int)source.ExpiresIn.TotalSeconds,
                RedirectUri = source.RedirectUri
            };
        }
    }
}
