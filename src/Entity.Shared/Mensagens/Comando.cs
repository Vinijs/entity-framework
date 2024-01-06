using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Shared.Mensagens
{
    public abstract class Comando : IRequisicao
    {
        public DateTime TimeStamp { get; set; }

        public Comando()
        {
            TimeStamp = DateTime.Now;
        }
    }
}