using AutoMapper;
using ScuffedAuth.Requests;
using TokenEndpoint = Authorization.TokenEndpoint;

namespace ScuffedAuth.Mapping
{
    public class RequestToModelProfile : Profile
    {
        public RequestToModelProfile()
        {
            CreateMap<TokenRequest, TokenEndpoint.TokenRequest>();
        }
    }
}
