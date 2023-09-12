using Rabbit.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using RabbitMQ.Client;
using System.Text.Json;
using Rabbit.API.Services.Interfaces;

namespace Rabbit.API.Services
{
    public class RabbitMensagemService : IRabbitMensagemService
    {
        public void SendMensagem(RabbitMensagem mensagem)
        {
            var factory = new ConnectionFactory { UserName = "admin", Password = "123456", HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "cadastro",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = JsonSerializer.Serialize(mensagem);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "cadastro",
                                 basicProperties: null,
                                 body: body);
            return;
        }
    }
}
