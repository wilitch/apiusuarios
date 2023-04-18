using ApiUsuarios.Domain.Interfaces.Producers;
using ApiUsuarios.Infra.RabbitMQ.Settings;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Infra.RabbitMQ.Producers
{
    /// <summary>
    /// Classe para implementar o producer da mensageria
    /// </summary>
    public class MessageQueueProducer : IMessageQueueProducer
    {
        public void Add(string message)
        {
            //capturando a connectionstring do servidor de mensageria
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(RabbitMQSettings.ConnectionString)
            };

            //conexão com o servidor da mensageria
            using (var connection = connectionFactory.CreateConnection())
            {
                //preencher os parametros para criar uma nova mensagem na fila
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: RabbitMQSettings.QueueName, //nome da fila
                        durable: true, //fila não apaga as mensagens
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    //escrevendo a mensagem na fila
                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: RabbitMQSettings.QueueName,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(message)
                        );
                }
            }
        }
    }
}



