using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class Pagamento
{
    public long PagamentoId { get; set; }

    public long PedidoId { get; set; }

    public int MetodoPagamento { get; set; }

    public int StatusPagamento { get; set; }

    public int Parcelas { get; set; }

    public decimal ValorPago { get; set; }

    public DateTime? DataPagamento { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;
}
