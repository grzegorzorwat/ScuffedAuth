using Microsoft.AspNetCore.Mvc;
using ScuffedAuth.Authorization.TokenEndpoint;
using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Controllers
{
    [ApiController]
    [Route("oauth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthorizationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("token")]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Token([FromHeader, Required] string authorization, [FromQuery] TokenRequest tokenRequest)
        {
            var response = _tokenService.GetToken(authorization, tokenRequest);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Token);
        }
    }
}
