using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Shared.IntegracaoEventos;
using Entity.Shared.Mensagens;

namespace Entity.Produtos.Application.Handlers
{
    public class ProdutosPedidosEventoHandler : INotificacaoHandler<PedidoFinalizadoEvento>
    {
        public void Handle(PedidoFinalizadoEvento evento)
        {
            //Logica para reservar estoque, emitir nota, acionar logistica
        }
    }
}