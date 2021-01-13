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

        [FromQuery(Name = "code")]
        public string? Code { get; set; }

        [FromQuery(Name = "client_id")]
        public string? ClientId { get; set; }

        [FromQuery(Name = "redirect_uri")]
        public string? RedirectUri { get; set; }
    }
}
