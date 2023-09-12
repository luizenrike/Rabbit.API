using Rabbit.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.API.Services
{
    public interface IRabbitMensagemService
    {
        void SendMensagem(RabbitMensagem mensagem);
    }
}
