using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.API.Models
{
    public class RabbitMensagem
    {
        public int Id { get; set; }
        public string EmailTo { get; set; }
        public string UserName { get; set; }
        public string Titulo { get; set; }
        public string Corpo { get; set; }   
    }
}
