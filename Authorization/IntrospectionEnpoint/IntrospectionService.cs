using Authorization.TokenEndpoint;
using BaseLibrary.Responses;
using System;
using System.Threading.Tasks;

namespace Authorization.IntrospectionEnpoint
{
    public class IntrospectionService : IIntrospectionService
    {
        private readonly ITokenRepository _tokenRepository;

        public IntrospectionService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<Response> Introspect(IntrospectionRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return new ErrorResponse("Invalid parameters");
            }

            var token = await _tokenRepository.GetToken(request.Token);

            if (token?.CreationDate.Add(token.ExpiresIn) > DateTime.UtcNow)
            {
                return new SuccessResponse<TokenInfoResource>(new TokenInfoResource(true));
            }

            return new SuccessResponse<TokenInfoResource>(new TokenInfoResource(false));
        }
    }
}
