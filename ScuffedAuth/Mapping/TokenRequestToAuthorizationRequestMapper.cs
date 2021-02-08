using BaseLibrary;
using ScuffedAuth.Requests;

namespace ScuffedAuth.Mapping
{
    public class TokenRequestToAuthorizationRequestMapper : IMapper<TokenRequest, Authorization.AuthorizationRequest>
    {
        public Authorization.AuthorizationRequest Map(TokenRequest source)
        {
            return new Authorization.AuthorizationRequest()
            {
                ClientId = source.ClientId,
                Code = source.Code,
                GrantType = source.GrantType,
                RedirectUri = source.RedirectUri
            };
        }
    }
}
