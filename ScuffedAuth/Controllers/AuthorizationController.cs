﻿using Authorization.IntrospectionEnpoint;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedAuth.Middlewares.Authentication;
using ScuffedAuth.Requests;
using System.Net;
using System.Threading.Tasks;
using AuthorizationEndpoint = Authorization.AuthorizationEndpoint;
using TokenEndpoint = Authorization.TokenEndpoint;

namespace ScuffedAuth.Controllers
{
    [ApiController]
    [Route("oauth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly TokenEndpoint.ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IIntrospectionService _introspectionService;
        private readonly AuthorizationEndpoint.IAuthorizationService _authorizationService;
        private readonly IAuthorizationService _authorization;

        public AuthorizationController(TokenEndpoint.ITokenService tokenService,
            IMapper mapper,
            IIntrospectionService introspectionService,
            AuthorizationEndpoint.IAuthorizationService authorizationService,
            IAuthorizationService authorization)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _introspectionService = introspectionService;
            _authorizationService = authorizationService;
            _authorization = authorization;
        }

        [HttpPost]
        [Route("token")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = AuthenticationSchemeConstants.GrantTypesAuthenticationScheme)]
        public async Task<ActionResult> Token([FromQuery] TokenRequest tokenRequest)
        {
            var authorizationRequest = _mapper.Map<TokenRequest, Authorization.AuthorizationRequest>(tokenRequest);
            var authorizationResponse = await _authorization.AuthorizeAsync(User,
                authorizationRequest,
                "GrantTypeAuthorization");

            if (!authorizationResponse.Succeeded)
            {
                return BadRequest();
            }

            var mappedRequest = _mapper.Map<TokenRequest, TokenEndpoint.TokenRequest>(tokenRequest);
            var response = await _tokenService.GetToken(mappedRequest);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var resource = _mapper.Map<TokenEndpoint.Token, TokenEndpoint.TokenResource>(response.Token);
            return Ok(resource);
        }

        [HttpPost]
        [Route("introspect")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<ActionResult> Introspect([FromQuery] IntrospectionRequest introspectionRequest)
        {
            var response = await _introspectionService.Introspect(introspectionRequest);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var resource = _mapper.Map<TokenInfo, TokenInfoResource>(response.TokenInfo);
            return Ok(resource);
        }

        [HttpGet]
        [Route("authorize")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult> Authorize([FromQuery] AuthorizationRequest authorizationRequest)
        {
            var mappedRequest = _mapper.Map<AuthorizationRequest, AuthorizationEndpoint.AuthorizationServiceRequest>(authorizationRequest);
            var response = await _authorizationService.Authorize(mappedRequest);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            if (User?.Identity?.IsAuthenticated != true)
            {
                string returnUrl = WebUtility.UrlEncode(HttpContext.Request.Path + HttpContext.Request.QueryString);
                return Redirect($"/Identity/Account/Login?ReturnUrl={returnUrl}");
            }

            var resource = _mapper.Map<AuthorizationEndpoint.AuthorizationCode, AuthorizationEndpoint.AuthorizationCodeResource>(response.AuthorizationCode);
            return Redirect(response.AuthorizationCode.RedirectUri + resource.ToQueryString());
        }
    }
}
