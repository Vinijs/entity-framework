﻿using System;
using System.Collections.Generic;

namespace Entity.Clientes.Domain.Entidades;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; }
    public string Email { get; set; }

    public string Observacao { get; set; }

    public int EnderecoId { get; set; }

    public DateTime? DataCadastro { get; set; }

    public virtual Endereco Endereco { get; set; }
}
