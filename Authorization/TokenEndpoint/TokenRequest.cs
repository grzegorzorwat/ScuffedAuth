﻿using OAuth.Model;

namespace Authorization.TokenEndpoint
{
    public class TokenRequest
    {
        public GrantTypes GrantType { get; set; }
    }
}
