using Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ScuffedAuth.Requests
{
    public class TokenRequest
    {
        [Required]
        [FromQuery(Name = "grant_type")]
        public GrantTypes GrantType { get; set; }
    }
}
