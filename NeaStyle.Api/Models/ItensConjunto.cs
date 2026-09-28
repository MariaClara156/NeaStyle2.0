using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class ItensConjunto
{
    public long ItemConjuntoId { get; set; }

    public int Quantidade { get; set; }

    public long? CarrinhoId { get; set; }

    public long? FavoritoId { get; set; }

    public long ProdutoVariacaoId { get; set; }

    public virtual Carrinho? Carrinho { get; set; }

    public virtual Favorito? Favorito { get; set; }

    public virtual ProdutoVariaco ProdutoVariacao { get; set; } = null!;
}
