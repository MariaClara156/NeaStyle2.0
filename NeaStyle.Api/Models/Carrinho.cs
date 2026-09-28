using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class Carrinho
{
    public long ConjuntoProdutoId { get; set; }

    public long ClienteId { get; set; }

    public bool Finalizado { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<ItensConjunto> ItensConjuntos { get; set; } = new List<ItensConjunto>();
}
