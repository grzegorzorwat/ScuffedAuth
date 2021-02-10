using BaseLibrary;
using System.Security.Claims;

namespace ScuffedAuth.Middlewares.Authentication
{
    internal interface IClaimsMapper<T> : IMapper<T, Claim[]>
    {
    }
}
