using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIChallenge.Models
{
    public class Response
    {
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }
        public Object Objeto { get; set; }
    }
}
