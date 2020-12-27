﻿using ScuffedAuth.Persistance;
using System.Threading.Tasks;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenService : ITokenService
    {
        private readonly AuthorizationFactory _authorizationFactory;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(AuthorizationFactory authorizationFactory,
            ITokenRepository tokenRepository,
            IUnitOfWork unitOfWork)
        {
            _authorizationFactory = authorizationFactory;
            _tokenRepository = tokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenResponse> GetToken(string authorizationHeader, TokenRequest request)
        {
            try
            {
                var authorization = _authorizationFactory.GetAuthorization(request.GrantType);
                var tokenResponse = await authorization.GetToken(authorizationHeader);

                if (tokenResponse.Success)
                {
                    await _tokenRepository.AddToken(tokenResponse.Token);
                    await _unitOfWork.Complete();
                }

                return tokenResponse;
            }
            catch
            {
                return new TokenResponse("An error occurred when saving the token.");
            }
        }
    }
}
