using ApiUsuarios.Application.Interfaces;
using ApiUsuarios.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriarContaController : ControllerBase
    {
        //atributos
        private readonly IUsuarioAppService? _usuarioAppService;

        //construtor para injeção de dependência
        public CriarContaController(IUsuarioAppService? usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
        }

        [HttpPost]
        public IActionResult Post(CriarContaModel model)
        {
            try
            {
                var usuario = _usuarioAppService.CriarUsuario(model);

                //HTTP 201 - CREATED
                return StatusCode(201, new
                {
                    mensagem = "Usuário cadastrado com sucesso.",
                    usuario //dados do usuário cadastrado
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



