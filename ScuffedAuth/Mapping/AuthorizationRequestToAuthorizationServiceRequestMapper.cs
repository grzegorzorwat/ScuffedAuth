using Authorization.AuthorizationEndpoint;
using BaseLibrary;
using ScuffedAuth.Requests;

namespace ScuffedAuth.Mapping
{
    public class AuthorizationRequestToAuthorizationServiceRequestMapper : IMapper<AuthorizationRequest, AuthorizationServiceRequest>
    {
        public AuthorizationServiceRequest Map(AuthorizationRequest source)
        {
            return new AuthorizationServiceRequest(source.ResponseType, source.ClientId, source.RedirectUri);
        }
    }
}
