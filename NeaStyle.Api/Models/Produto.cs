using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class Produto
{
    public long ProdutoId { get; set; }

    public string Nome { get; set; } = null!;

    public int Tipo { get; set; }

    public int Categoria { get; set; }

    public string Descricao { get; set; } = null!;

    public bool Ativo { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<ProdutoVariaco> ProdutoVariacos { get; set; } = new List<ProdutoVariaco>();
}
