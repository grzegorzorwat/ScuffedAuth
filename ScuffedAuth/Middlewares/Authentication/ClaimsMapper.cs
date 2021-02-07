using Authentication;
using System.Security.Claims;

namespace ScuffedAuth.Middlewares.Authentication
{
    public class ClaimsMapper : IClaimsMapper<ResponseClient>
    {
        public Claim[] Map(ResponseClient source)
        {
            return new[]
            {
                new Claim(ClaimTypes.NameIdentifier, source.Id)
            };
        }
    }
}
