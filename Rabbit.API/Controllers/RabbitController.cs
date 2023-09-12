using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rabbit.API.Models;
using Rabbit.API.Services;

namespace Rabbit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly IRabbitMensagemService _rabbitMensagemService;

        public RabbitController(IRabbitMensagemService rabbitMensagemService)
        {
            _rabbitMensagemService = rabbitMensagemService;
        }
        //[HttpPost]
        //public void SendMensagem(RabbitMensagem mensagem)
        //{
        //    _rabbitMensagemService.SendMensagem(mensagem);
        //}
    }
}
