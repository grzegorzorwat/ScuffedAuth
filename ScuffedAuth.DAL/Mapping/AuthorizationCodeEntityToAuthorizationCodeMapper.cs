using Authorization.AuthorizationCode;
using BaseLibrary;
using ScuffedAuth.DAL.Entities;

namespace ScuffedAuth.DAL.Mapping
{
    internal class AuthorizationCodeEntityToAuthorizationCodeMapper : IMapper<AuthorizationCodeEntity, AuthorizationCode>
    {
        public AuthorizationCode Map(AuthorizationCodeEntity source)
        {
            if (source == null)
            {
                return null;
            }

            return new AuthorizationCode()
            {
                ClientId = source.ClientId,
                Code = source.Code,
                CreationDate = source.CreationDate,
                ExpiresIn = source.ExpiresIn,
                RedirectUri = source.RedirectUri
            };
        }
    }
}
