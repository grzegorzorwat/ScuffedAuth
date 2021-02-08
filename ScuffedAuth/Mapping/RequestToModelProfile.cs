using AutoMapper;
using ScuffedAuth.Requests;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;
using TokenEndpoint = Authorization.TokenEndpoint;

namespace ScuffedAuth.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<TokenRequest, TokenEndpoint.TokenRequest>();
            CreateMap<TokenRequest, Authorization.AuthorizationRequest>();
            CreateMap<AuthorizationRequest, AuthorizationEndpoint.AuthorizationServiceRequest>();
        }
    }
}
