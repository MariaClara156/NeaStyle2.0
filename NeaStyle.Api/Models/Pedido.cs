using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class Pedido
{
    public long PedidoId { get; set; }

    public long ClienteId { get; set; }

    public DateTime DataPedido { get; set; }

    public decimal ValorTotal { get; set; }

    public int Status { get; set; }

    public long? ProdutoId { get; set; }

    public long? ClienteUsuarioId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Cliente? ClienteUsuario { get; set; }

    public virtual ICollection<ItensPedido> ItensPedidos { get; set; } = new List<ItensPedido>();

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();

    public virtual Produto? Produto { get; set; }

    public virtual ICollection<Reembolso> Reembolsos { get; set; } = new List<Reembolso>();
}
