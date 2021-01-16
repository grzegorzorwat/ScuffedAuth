using Authorization.TokenEndpoint;
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

        public async Task<IntrospectionResponse> Introspect(IntrospectionRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return new IntrospectionResponse("Invalid parameters");
            }

            var token = await _tokenRepository.GetToken(request.Token);

            if (token?.CreationDate.Add(token.ExpiresIn) > DateTime.UtcNow)
            {
                return new IntrospectionResponse(new TokenInfo(request.Token, true));
            }

            return new IntrospectionResponse(new TokenInfo(request.Token, false));
        }
    }
}
