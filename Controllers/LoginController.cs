using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Gufos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Gufos.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class LoginController : ControllerBase {
        GufosContext _context = new GufosContext ();

        private IConfiguration _config;

        public LoginController (IConfiguration config)
         {
            _config = config;
         }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login (Usuario login) 
        {
            IActionResult resposta = Unauthorized();

            var usuario = autenticarUsuario(login);

            if (usuario != null)
            {
                var tokenLinha = gerarJsonWebToken(usuario);
                resposta = Ok(new {token = tokenLinha});
            }
            return resposta;
        }
       /// <summary>
       /// Método privado que valida se um usuário exixte no nosso banco de dados
       /// </summary>
       /// <param name="login">Objeto do tipo usuário</param>
       /// <returns>Objeto do tipo Usuário</returns>
        private Usuario autenticarUsuario(Usuario login)
        {
            var usuario = _context.Usuario.FirstOrDefault(user => user.Email == login.Email && user.Senha == login.Senha);
            if (usuario != null)
            {
                return login;
            }
            return usuario;
        }
        private string gerarJsonWebToken(Usuario infoUsuario){
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var claims = new []{
            new Claim(JwtRegisteredClaimNames.NameId, infoUsuario.Nome),
            new Claim(JwtRegisteredClaimNames.Email, infoUsuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], 
            _config["Jwt:Issuer"],
            claims, 
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials:credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}