using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rapsody.Api.Auth;
using Rapsody.Api.Models;
using System.Text;

namespace Rapsody.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        [Route("token")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Token([FromBody]Login login)
        {
            if (login.UserName == "admin" && login.Password == "socgen123")
            {

                var token = new JwtTokenBuilder()
                                    .AddSecurityKey(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("rapsody-secret-key")))
                                    .AddSubject("rapsody")
                                    .AddIssuer("Rapsody.Security.Bearer")
                                    .AddAudience("Rapsody.Security.Bearer")
                                    .AddExpiry(5)
                                    .Build();

                return Ok(token.Value);
            }

            return Unauthorized();
        }
    }
}