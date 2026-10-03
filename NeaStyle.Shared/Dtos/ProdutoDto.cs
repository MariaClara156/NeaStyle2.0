namespace NeaStyle.Shared.Dtos;

public class ProdutoDto
{
    public long ProdutoId { get; set; }
    public string Nome { get; set; } = null!;
    public int Tipo { get; set; }
    public int Categoria { get; set; }
    public string Descricao { get; set; } = null!;
    public bool Ativo { get; set; }
    public List<ProdutoVariacaoDto> Variacoes { get; set; } = new();
}

public class CreateProdutoDto
{
    public string Nome { get; set; } = null!;
    public int Tipo { get; set; }
    public int Categoria { get; set; }
    public string Descricao { get; set; } = null!;
}

public class UpdateProdutoDto
{
    public string Nome { get; set; } = null!;
    public int Tipo { get; set; }
    public int Categoria { get; set; }
    public string Descricao { get; set; } = null!;
    public bool Ativo { get; set; }
}