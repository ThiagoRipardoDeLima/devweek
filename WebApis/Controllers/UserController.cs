using Entities.Entities;
using Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using WebApis.Models;
using WebApis.Token;

namespace WebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [Produces("Application/json")]
        [HttpPost("/api/CriarTokenIdenty")]
        public async Task<IActionResult> CriarTokenIdenty([FromBody] Login login)
        {
            if(string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
            {
                return Unauthorized();
            }

            var resultado = await
                   _signInManager.PasswordSignInAsync(login.email, login.senha, false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(login.email);
                var idUsuario = user.Id;

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-123456"))
                    .AddSubject("Empresa Ficticia Inexistente")
                    .AddIssuer("EmpresaFicticia.Security.Bearer")
                    .AddAudience("EmpresaFicticia.Security.Bearer")
                    .AddClaim("idUsuario", idUsuario)
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }

            return Unauthorized();

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionarUsuarioIdentity")]
        public async Task<IActionResult> AdicionarUsuarioIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.senha))
                return Ok("Falta alguns dados");

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                CPF = login.cpf,
                Tipo = TipoUsuario.Comum
            };

            var resultado = await _userManager.CreateAsync(user, login.senha);

            if(resultado.Errors.Any())
                return Ok(resultado.Errors);

            //Geração de Confirmação caso precise
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var emailConfirmation = await _userManager.ConfirmEmailAsync(user, code);

            if (emailConfirmation.Succeeded)
                return Ok("Usuário Adicionado com Sucesso");

            return Ok("Erro ao confirmar usuário");
        }

    }

}
