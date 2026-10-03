namespace NeaStyle.Shared.Dtos;

public class ProdutoVariacaoDto
{
    public long ProdutoVariacaoId { get; set; }
    public string Tamanho { get; set; } = null!;
    public string Cor { get; set; } = null!;
    public int Estoque { get; set; }
    public string? ImagemUrl { get; set; }
    public decimal Preco { get; set; }
}

public class CreateProdutoVariacaoDto
{
    public string Tamanho { get; set; } = null!;
    public string Cor { get; set; } = null!;
    public int Estoque { get; set; }
    public int EstoqueMinimo { get; set; } = 5;
    public string? ImagemUrl { get; set; }
    public decimal Preco { get; set; }
    public decimal PrecoCusto { get; set; }
}