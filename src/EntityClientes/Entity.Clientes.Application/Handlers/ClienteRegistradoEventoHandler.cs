using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Shared.Mensagens;
using Entity.Clientes.Application.Events;

namespace Entity.Clientes.Application.Handlers
{
    public class ClienteRegistradoEventoHandler : INotificacaoHandler<ClienteRegistradoEvento>
    {
        public ClienteRegistradoEventoHandler()
        {
            
        }
        
        public void Handle(ClienteRegistradoEvento evento)
        {
            //Implentar uma logica de envio de email
        }
    }
}