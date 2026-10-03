using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class ProdutoVariaco
{
    public long ProdutoVariacaoId { get; set; }

    public string Tamanho { get; set; } = null!;

    public string Cor { get; set; } = null!;

    public int Estoque { get; set; }

    public long ProdutoId { get; set; }

    public string? ImagemUrl { get; set; }

    public decimal Preco { get; set; }

    public decimal PrecoCusto { get; set; }

    public int EstoqueMinimo { get; set; }

    public virtual ICollection<ItensConjunto> ItensConjuntos { get; set; } = new List<ItensConjunto>();

    public virtual ICollection<MovimentacoesEstoque> MovimentacoesEstoques { get; set; } = new List<MovimentacoesEstoque>();

    public virtual Produto Produto { get; set; } = null!;
}
