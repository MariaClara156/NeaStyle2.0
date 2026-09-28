using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class ItensPedido
{
    public long ItemPedidoId { get; set; }

    public long PedidoId { get; set; }

    public long ProdutoVariacaoId { get; set; }

    public int Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    public string? ImagemUrl { get; set; }

    public string NomeProduto { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;
}
