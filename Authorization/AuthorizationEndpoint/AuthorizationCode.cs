using Authorization.Codes;
using System;

namespace Authorization.AuthorizationEndpoint
{
    public class AuthorizationCode : ExpiringCode
    {
        private string? _clientId;
        private string? _redirectUri;

        public string ClientId 
        { 
            get
            {
                if(_clientId is null)
                {
                    throw new InvalidOperationException($"{nameof(ClientId)} is not set");
                }

                return _clientId;
            }
            set
            {
                if(_clientId is not null)
                {
                    throw new InvalidOperationException($"{nameof(ClientId)} is already set");
                }

                _clientId = value;
            }
        }

        public string RedirectUri
        {
            get
            {
                if (_redirectUri is null)
                {
                    throw new InvalidOperationException($"{nameof(RedirectUri)} is not set");
                }

                return _redirectUri;
            }
            set
            {
                if (_redirectUri is not null)
                {
                    throw new InvalidOperationException($"{nameof(RedirectUri)} is already set");
                }

                _redirectUri = value;
            }
        }
    }
}
