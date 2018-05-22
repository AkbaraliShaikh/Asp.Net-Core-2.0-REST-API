using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rapsody.Api.Auth;
using Rapsody.Api.Models;
using System;
using System.Text;

namespace Rapsody.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("token")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Token([FromBody]Login login)
        {
            int expiry = 10;

            if (_configuration["Authentication:Token:Expiry"] != null)
                expiry = Convert.ToInt32(_configuration["Authentication:Token:Expiry"]);

                if (login.UserName == "admin" && login.Password == "socgen123")
                {
                    var token = new JwtTokenBuilder()
                                        .AddSecurityKey(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("rapsody-secret-key")))
                                        .AddSubject("rapsody")
                                        .AddIssuer("Rapsody.Security.Bearer")
                                        .AddAudience("Rapsody.Security.Bearer")
                                        .AddExpiry(expiry)
                                        .Build();

                    return Ok(new { access_token = token.Value, expires_in = 600, token_type = "Bearer" });
                }

            return Unauthorized();
        }
    }
}