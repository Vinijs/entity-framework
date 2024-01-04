using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Produtos.Entidades
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new List<Produto>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        //EF - Relacionamento
        public virtual List<Produto> Produtos {get; set;}

        
    }
}