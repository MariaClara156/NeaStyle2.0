using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class Reembolso
{
    public long ReembolsoId { get; set; }

    public long PedidoId { get; set; }

    public string Motivo { get; set; } = null!;

    public DateTime DataSolicitacao { get; set; }

    public decimal ValorReembolso { get; set; }

    public int Status { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;
}
