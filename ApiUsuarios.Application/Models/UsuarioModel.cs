using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Application.Models
{
    /// <summary>
    /// Modelo de dados para retornar informações do usuário
    /// </summary>
    public class UsuarioModel
    {
        public Guid? IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraCriacao { get; set; }
    }
}



