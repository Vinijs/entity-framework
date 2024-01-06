using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Shared.Handlers;
using Entity.Shared.Mensagens;

namespace Entity.Shared.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T: INotificacao;
        Task EnviarComando<T>(T comado) where T: IRequisicao;
        Task RegistrarEventoHandler<T>(INotificacaoHandler<T> handler) where T : INotificacao;
        Task RegistrarComandoHandler<T>(IRequisicaoHandler<T> handler) where T: IRequisicao;
    }
}