using System.Security.Claims;

namespace ScuffedAuth.Middlewares.Authentication
{
    internal interface IClaimsMapper<T>
    {
        Claim[] Map(T source);
    }
}
