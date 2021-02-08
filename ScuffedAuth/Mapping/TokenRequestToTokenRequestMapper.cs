using BaseLibrary;
using ScuffedAuth.Requests;
using TokenEndpoint = Authorization.TokenEndpoint;

namespace ScuffedAuth.Mapping
{
    public class TokenRequestToTokenRequestMapper : IMapper<TokenRequest, TokenEndpoint.TokenRequest>
    {
        public TokenEndpoint.TokenRequest Map(TokenRequest source)
        {
            return new TokenEndpoint.TokenRequest()
            {
                GrantType = source.GrantType
            };
        }
    }
}
