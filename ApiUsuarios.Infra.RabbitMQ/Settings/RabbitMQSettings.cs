using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Infra.RabbitMQ.Settings
{
    public class RabbitMQSettings
    {
        /// <summary>
        /// Endereço para conexão no servidor de mensageria
        /// </summary>
        public static string? ConnectionString 
        { 
            get => @"amqps://hnpgpotz:UhTcOrSTLz9g0eLzReo1nHIZQ3B9sAZv@woodpecker.rmq.cloudamqp.com/hnpgpotz"; 
        }

        /// <summary>
        /// Nome da fila em que iremos conectar a aplicação
        /// </summary>
        public static string? QueueName 
        { 
            get => "recuperacao_de_senha";
        }
    }
}



