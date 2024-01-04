﻿using System;
using System.Collections.Generic;

namespace Entity.Produtos.Entidades;

public partial class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? UrlImagem { get; set; }

    public string? Descricao { get; set; }

    public double Valor { get; set; }
    public int CategoriaId { get; set; }

    public int FornecedorId { get; set; }

    //EF - Relacionamento
    //Ao definir uma propriedade de navegação com virtual estamos habilitando o lazy load
    public virtual Categoria Categoria {get; set;}
    public virtual  Fornecedor Fornecedor { get; set; }
}
