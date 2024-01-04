using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Pedidos.Domain.Entidades
{
    [Table("cupom_descontos")]
    public class CupomDesconto
    {
        public CupomDesconto()
        {
            
        }

        public int CupomDescontoId {get;set;}
        public string CodigoCupom { get; set; }
        public decimal? PercentualDesconto { get; set; }
        public decimal? ValorDesconto {get;set;}
        public int Quantidade {get;set;} //quantidade de cupons disponiveis para serem aplicados
        public TipoCupomDesconto TipoCupomDesconto {get;set;}
        public DateTime CriadoEm {get;set;}
        public DateTime AplicadoEm {get;set;}
        public DateTime DataExpiracao {get;set;}
        public bool Ativo {get;set;}
        public bool Aplicado {get;set;}

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
    public enum TipoCupomDesconto
    {
        Percentual = 0,
        Valor = 1
    }
    
}