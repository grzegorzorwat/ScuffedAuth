using Authorization.IntrospectionEnpoint;
using AutoMapper;
using BaseLibrary;
using BaseLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScuffedAuth.Middlewares.Authentication;
using ScuffedAuth.Requests;
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
        private readonly IResponseVisitor<ActionResult> _responseActionResultVisitor;
        private readonly IMapper<TokenRequest, TokenEndpoint.TokenRequest> _tokenRequestMapper;
        private readonly IMapper<TokenRequest, Authorization.AuthorizationRequest> _authorizationRequestMapper;

        public AuthorizationController(TokenEndpoint.ITokenService tokenService,
            IMapper mapper,
            IIntrospectionService introspectionService,
            AuthorizationEndpoint.IAuthorizationService authorizationService,
            IAuthorizationService authorization,
            IResponseVisitor<ActionResult> responseActionResultVisitor,
            IMapper<TokenRequest, TokenEndpoint.TokenRequest> tokenRequestMapper,
            IMapper<TokenRequest, Authorization.AuthorizationRequest> authorizationRequestMapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _introspectionService = introspectionService;
            _authorizationService = authorizationService;
            _authorization = authorization;
            _responseActionResultVisitor = responseActionResultVisitor;
            _tokenRequestMapper = tokenRequestMapper;
            _authorizationRequestMapper = authorizationRequestMapper;
        }

        [HttpPost]
        [Route("token")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = AuthenticationSchemeConstants.GrantTypesAuthenticationScheme)]
        public async Task<ActionResult> Token([FromQuery] TokenRequest tokenRequest)
        {
            var authorizationRequest = _authorizationRequestMapper.Map(tokenRequest);
            var authorizationResponse = await _authorization.AuthorizeAsync(User,
                authorizationRequest,
                "GrantTypeAuthorization");

            if (!authorizationResponse.Succeeded)
            {
                return BadRequest();
            }

            var mappedRequest = _tokenRequestMapper.Map(tokenRequest);
            var response = await _tokenService.GetToken(mappedRequest);
            return response.Accept(_responseActionResultVisitor);
        }

        [HttpPost]
        [Route("introspect")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<ActionResult> Introspect([FromQuery] IntrospectionRequest introspectionRequest)
        {
            var response = await _introspectionService.Introspect(introspectionRequest);
            return response.Accept(_responseActionResultVisitor);
        }

        [HttpGet]
        [Route("authorize")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<ActionResult> Authorize([FromQuery] AuthorizationRequest authorizationRequest)
        {
            var mappedRequest = _mapper.Map<AuthorizationRequest, AuthorizationEndpoint.AuthorizationServiceRequest>(authorizationRequest);
            var response = await _authorizationService.Authorize(mappedRequest);
            return response.Accept(_responseActionResultVisitor);
        }
    }
}
