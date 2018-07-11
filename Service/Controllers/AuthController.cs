using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    [Authorize]
    public class AuthController : Controller
    {
        // GET: api/Auth
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<JwtSecurityToken> Get()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Angular"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: "Issuer",
                audience: "Audience",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PL6TsZZY36hWXMssSzNydYXYB9KF")),
                        SecurityAlgorithms.HmacSha256)
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
