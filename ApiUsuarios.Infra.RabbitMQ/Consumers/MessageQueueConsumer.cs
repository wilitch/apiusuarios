using ApiUsuarios.Domain.Models;
using ApiUsuarios.Infra.Messages.Services;
using ApiUsuarios.Infra.RabbitMQ.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.Infra.RabbitMQ.Consumers
{
    /// <summary>
    /// Classe para processar a fila do servidor de mensageria
    /// </summary>
    public class MessageQueueConsumer : BackgroundService
    {
        //atributos
        private readonly IServiceProvider? _serviceProvider;
        private readonly IConnection? _connection;
        private readonly IModel? _model;

        public MessageQueueConsumer(IServiceProvider? serviceProvider)
        {
            _serviceProvider = serviceProvider;

            //definindo o local onde esta o servidor da mensageria
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(RabbitMQSettings.ConnectionString)
            };

            //conectando no servidor da mensageria
            _connection = connectionFactory.CreateConnection();
            //acessando a fila
            _model = _connection.CreateModel();
            _model.QueueDeclare(
                queue: RabbitMQSettings.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }

        //método utilizado para ler a fila
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //objeto para ler o conteudo da fila
            var consumer = new EventingBasicConsumer(_model);

            //fazendo a leitura
            consumer.Received += (sender, args) =>
            {
                //ler a mensagem gravada na fila
                var contentArray = args.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

                //deserializando o conteudo
                var messageModel = JsonConvert.DeserializeObject<MessageModel>(contentString);

                //enviando o email para o usuário
                using (var scope = _serviceProvider.CreateScope())
                {
                    var emailMessage = new EmailMessage();
                    emailMessage.Send(messageModel.EmailDest, messageModel.Assunto, messageModel.Conteudo);

                    //retirar a mensagem da fila
                    _model.BasicAck(args.DeliveryTag, false);
                }
            };

            _model.BasicConsume(RabbitMQSettings.QueueName, false, consumer);
            return Task.CompletedTask;
        }
    }
}



