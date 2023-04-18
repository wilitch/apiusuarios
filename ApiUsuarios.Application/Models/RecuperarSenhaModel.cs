using System.ComponentModel.DataAnnotations;

namespace ApiUsuarios.Application.Models
{
    public class RecuperarSenhaModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }
    }
}
