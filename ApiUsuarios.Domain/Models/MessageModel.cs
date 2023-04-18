using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Domain.Models
{
    public class MessageModel
    {
        public string? EmailDest { get; set; }
        public string? Assunto { get; set; }
        public string? Conteudo { get; set; }
    }
}



