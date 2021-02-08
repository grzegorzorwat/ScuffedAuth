using AutoMapper;
using ScuffedAuth.Requests;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;

namespace ScuffedAuth.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<AuthorizationRequest, AuthorizationEndpoint.AuthorizationServiceRequest>();
        }
    }
}
