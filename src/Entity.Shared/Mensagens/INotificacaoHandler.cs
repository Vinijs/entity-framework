using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Shared.Mensagens
{
    public interface INotificacaoHandler<T> where T: INotificacao
    {
        public void Handle(T evento);
    }
}