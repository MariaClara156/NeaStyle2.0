using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class MovimentacoesEstoque
{
    public long MovimentacaoId { get; set; }

    public long ProdutoVariacaoId { get; set; }

    public int QuantidadeAnterior { get; set; }

    public int QuantidadeNova { get; set; }

    public string TipoMovimentacao { get; set; } = null!;

    public DateTime DataMovimentacao { get; set; }

    public virtual ProdutoVariaco ProdutoVariacao { get; set; } = null!;
}
