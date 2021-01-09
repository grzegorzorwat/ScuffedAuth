using Authorization.AuthorizationEndpoint;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ScuffedAuth.Requests
{
    public class AuthorizationRequest
    {
        [Required]
        [FromQuery(Name = "response_type")]
        public ResponseType ResponseType { get; set; }

        [Required]
        [FromQuery(Name = "client_id")]
        [NotNull] public string? ClientId { get; set; }
    }
}
