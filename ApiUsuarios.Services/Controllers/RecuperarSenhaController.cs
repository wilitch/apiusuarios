using ApiUsuarios.Application.Interfaces;
using ApiUsuarios.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecuperarSenhaController : ControllerBase
    {
        //atributo
        private readonly IUsuarioAppService? _usuarioAppService;

        //construtor para injeção de dependência
        public RecuperarSenhaController(IUsuarioAppService? usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        public IActionResult Post(RecuperarSenhaModel model)
        {
            try
            {
                var usuario = _usuarioAppService.RecuperarSenha(model);
                return StatusCode(200, new
                {
                    mensagem = "Recuperação de senha realizado com sucesso.",
                    usuario
                });
            }
            catch (ArgumentException e)
            {
                //HTTP 400 - BAD REQUEST
                return StatusCode(400, new { mensagem = e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 - INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}



