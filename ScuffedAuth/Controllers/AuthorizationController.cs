using Microsoft.AspNetCore.Mvc;
using ScuffedAuth.Authorization.ClientCredentials;
using ScuffedAuth.Authorization.TokenEndpoint;
using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Controllers
{
    [ApiController]
    [Route("oauth")]
    public class AuthorizationController : ControllerBase
    {
        public AuthorizationController()
        {
        }

        [HttpPost]
        [Route("token")]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult Token([FromHeader, Required] string authorization, [FromQuery] TokenRequest tokenRequest)
        {
            return Ok();
        }
    }
}
