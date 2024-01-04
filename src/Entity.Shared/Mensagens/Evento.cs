using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Shared.Mensagens
{
    public abstract class Evento : INotificacao
    {
        public DateTime TimeStamp { get; set; }
        public Evento()
        {
            TimeStamp = DateTime.Now;
        }
    }
}