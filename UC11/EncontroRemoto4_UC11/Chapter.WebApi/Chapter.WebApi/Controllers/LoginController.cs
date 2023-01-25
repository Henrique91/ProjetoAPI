using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using Chapter.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Chapter.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepesository;
        public LoginController(IUsuarioRepository iUsuarioRepository) 
        {
            _iUsuarioRepesository= iUsuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
                Usuario usuarioBuscado = _iUsuarioRepesository.Login(login.Email, login.Senha);

                if (usuarioBuscado == null)
                {
                    return Unauthorized(new { msg = "Email e/ou senha inválidos, tente novamente!" });
                }

                //Define os dados que serão fornecidos no token (Payload)
                var minhasClaims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.Tipo)
                };

                //define a chave de acesso ao token
                var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter.WebApi-chave-autenticacao"));

                //define as credenciais do token (header)
                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                //gera o token
                var meuToken = new JwtSecurityToken(
                    issuer: "Chapter.WebApi",
                    audience: "Chapter.WebApi",
                    claims: minhasClaims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credenciais
                    );

                return Ok( 
                    new {token = new JwtSecurityTokenHandler().WriteToken(meuToken)}
                    );
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }
    }
}
