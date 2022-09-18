using apiGXCFernandoM.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using apiGXCFernandoM.Models;
using apiGXCFernandoM.Exeptions;
using apiGXCFernandoM.Tools;

namespace apiGXCFernandoM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private ColegioContext dbcontext;

        public LoginController(IConfiguration config, ColegioContext dbcontext)
        {
            _config = config;
            this.dbcontext = dbcontext;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                TokenUsu tokenUsu = new TokenUsu();
                tokenUsu.token = Generate(user);
                return Ok(tokenUsu);
            }

            return NotFound(Constants.SOLDENEG);
        }


        private string Generate(Usuario user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.username),
                new Claim(ClaimTypes.Email, user.correoElectronico),
                new Claim(ClaimTypes.GivenName, user.nombreCompleto),
                new Claim(ClaimTypes.Role, user.rol)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Usuario Authenticate(UserLogin userLogin)
        {
            try
            {
                var currentUser = dbcontext.Usuarios                                                    .First(o => o.username ==
                userLogin.UserName && o.password ==(userLogin.Password));

                if (currentUser != null)
                {
                    return currentUser;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
