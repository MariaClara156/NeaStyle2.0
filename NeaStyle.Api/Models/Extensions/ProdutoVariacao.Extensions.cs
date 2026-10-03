namespace NeaStyle.Api.Models;

public partial class ProdutoVariaco
{
    
    public bool EmEstoqueBaixo => Estoque <= EstoqueMinimo;

    public decimal MargemLucro => Preco - PrecoCusto;
}