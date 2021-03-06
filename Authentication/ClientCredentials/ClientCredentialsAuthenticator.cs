﻿using BaseLibrary.Responses;
using System.Threading.Tasks;

namespace Authentication.ClientCredentials
{
    internal class ClientCredentialsAuthenticator : IAuthenticator
    {
        private readonly ClientCredentialsDecoder _decoder;
        private readonly IClientsRepository _clientsRepository;
        private readonly ISecretVerifier _secretVerifier;

        public ClientCredentialsAuthenticator(ClientCredentialsDecoder decoder,
            IClientsRepository clientsRepository,
            ISecretVerifier secretVerifier)
        {
            _decoder = decoder;
            _clientsRepository = clientsRepository;
            _secretVerifier = secretVerifier;
        }

        public async Task<Response> Authenticate(string authorizationHeader)
        {
            if (_decoder.TryDecode(authorizationHeader, out var credentials))
            {
                var client = await _clientsRepository.GetClientByIdAsync(credentials.Id);

                if (client is not null)
                {
                    if (_secretVerifier.Verify(client.Secret, credentials.Secret))
                    {
                        return new SuccessResponse<ResponseClient>(new ResponseClient(client.Id));
                    }
                }
            }

            return new ErrorResponse("Invalid credentials");
        }
    }
}
