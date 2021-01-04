using Authorization.AuthorizationEndpoint;
using Authorization.IntrospectionEnpoint;
using Authorization.TokenEndpoint;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedAuth.Authentication;
using System.Threading.Tasks;

namespace ScuffedAuth.Controllers
{
    [ApiController]
    [Route("oauth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IIntrospectionService _introspectionService;
        private readonly Authorization.AuthorizationEndpoint.IAuthorizationService _authorizationService;

        public AuthorizationController(ITokenService tokenService,
            IMapper mapper,
            IIntrospectionService introspectionService,
            Authorization.AuthorizationEndpoint.IAuthorizationService authorizationService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _introspectionService = introspectionService;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("token")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = AuthenticationSchemeConstants.GrantTypesAuthenticationScheme)]
        public async Task<ActionResult> Token([FromQuery] TokenRequest tokenRequest)
        {
            var response = await _tokenService.GetToken(tokenRequest);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var resource = _mapper.Map<Token, TokenResource>(response.Token);
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
            var response = await _authorizationService.Authorize(authorizationRequest);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var resource = _mapper.Map<AuthorizationCode, AuthorizationCodeResource>(response.AuthorizationCode);
            return Ok(resource);
        }
    }
}
