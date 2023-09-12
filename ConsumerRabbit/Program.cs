using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Json;
using Rabbit.API.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rabbit.API.Configurations;

var factory = new ConnectionFactory { UserName = "admin", Password = "123456", HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "cadastro",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = JsonSerializer.Deserialize<RabbitMensagem>(body);
    Console.WriteLine($" [x] Mensagem recebida= Id: {message.Id}, Titulo: {message.Titulo}, Corpo: {message.Corpo}");
    SendEmail(message);
};
channel.BasicConsume(queue: "cadastro",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

void SendEmail(RabbitMensagem mensagem)
{
    SmtpClient MyServer = new SmtpClient();
    MyServer.Port = 587;
    MyServer.Host = "smtp.office365.com";
    MyServer.UseDefaultCredentials = false;

    EmailCredentials emailCredentials = new EmailCredentials();
    
   
    NetworkCredential credential = new NetworkCredential(emailCredentials.email, emailCredentials.password);

    MyServer.Credentials = credential;
    MyServer.EnableSsl = true; 

    MailAddress from = new MailAddress(emailCredentials.email, "Rabbit.API Application Test");
    MailAddress to = new MailAddress(mensagem.EmailTo, mensagem.UserName);

    MailMessage message = new MailMessage(from, to);
    message.Subject = mensagem.Titulo;
    message.Body = mensagem.Corpo;

    MyServer.Send(message);
    MyServer.Dispose();
}