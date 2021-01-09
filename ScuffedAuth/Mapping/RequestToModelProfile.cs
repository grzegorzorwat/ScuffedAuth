using AutoMapper;
using ScuffedAuth.Requests;
using TokenEndpoint = Authorization.TokenEndpoint;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;

namespace ScuffedAuth.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<TokenRequest, TokenEndpoint.TokenRequest>();
            CreateMap<AuthorizationRequest, AuthorizationEndpoint.AuthorizationRequest>();
        }
    }
}
