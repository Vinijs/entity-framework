using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Pedidos.Domain.Entidades
{
    [Table("pedido_itens")]
    public partial class PedidoItem
    {
        public int PedidoItemId {get;set;}
        public int PedidoId {get;set;}
        public int ProdutoId {get;set;}
        public string NomeProduto {get;set;}
        public int Quantidade {get;set;}
        public decimal ValorUnitario { get; set; }

        public virtual Pedido Pedido { get; set; }
    }
}