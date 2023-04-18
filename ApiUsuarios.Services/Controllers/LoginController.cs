using ApiUsuarios.Application.Interfaces;
using ApiUsuarios.Application.Models;
using ApiUsuarios.Services.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //atributo
        private readonly IUsuarioAppService? _usuarioAppService;

        //construtor para injeção de dependência
        public LoginController(IUsuarioAppService? usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        public IActionResult Post(LoginModel model)
        {
            try
            {
                var tokenCreator = new TokenCreator();

                var usuario = _usuarioAppService.Autenticar(model);
                return StatusCode(200, new
                {
                    mensagem = "Usuário autenticado com sucesso.",
                    usuario,
                    token = tokenCreator.GenerateToken(usuario.Email)
                });
            }
            catch (ArgumentException e)
            {
                //HTTP 401 - UNAUTHORIZED
                return StatusCode(401, new { mensagem = e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}



