using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScuffedAuth.Authorization.TokenEndpoint;
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

        public AuthorizationController(ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
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
    }
}
