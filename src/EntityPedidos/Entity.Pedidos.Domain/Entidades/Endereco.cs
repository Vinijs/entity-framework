using System;
using System.Collections.Generic;

namespace Entity.Pedidos.Domain.Entidades;

public partial class Endereco
{
    public int Id { get; set; }

    public string Logradouro { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string Complemento { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
