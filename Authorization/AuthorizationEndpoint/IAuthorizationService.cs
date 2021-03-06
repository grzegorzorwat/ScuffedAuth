﻿using BaseLibrary.Responses;
using System.Threading.Tasks;

namespace Authorization.AuthorizationEndpoint
{
    public interface IAuthorizationService
    {
        Task<Response> Authorize(AuthorizationServiceRequest request);
    }
}
