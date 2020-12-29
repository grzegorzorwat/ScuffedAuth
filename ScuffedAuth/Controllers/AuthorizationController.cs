using Authorization.IntrospectionEnpoint;
using Authorization.TokenEndpoint;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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

        public AuthorizationController(ITokenService tokenService,
            IMapper mapper,
            IIntrospectionService introspectionService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _introspectionService = introspectionService;
        }

        [HttpPost]
        [Route("token")]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<ActionResult> Token([FromHeader, Required] string authorization, [FromQuery] TokenRequest tokenRequest)
        {
            var response = await _tokenService.GetToken(authorization, tokenRequest);

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
    }
}
