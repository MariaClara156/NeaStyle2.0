namespace NeaStyle.Api.Models;

public partial class Produto
{

    public bool TemVariacoesDisponiveis()
        => ProdutoVariacos.Any(v => v.Estoque > 0);
}