using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Authorization.TokenEndpoint
{
    public class TokenRequest
    {
        [FromQuery]
        public GrantTypes GrantType { get; set; }
    }
}
