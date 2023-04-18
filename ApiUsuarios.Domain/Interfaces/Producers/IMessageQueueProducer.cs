using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Domain.Interfaces.Producers
{
    public interface IMessageQueueProducer
    {
        /// <summary>
        /// Método para adicionar um item na fila de mensageria
        /// </summary>
        /// <param name="message">Conteúdo que será escrito na fila</param>
        void Add(string message);
    }
}



