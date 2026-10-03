using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeaStyle.Api.Data;
using NeaStyle.Api.Models;
using NeaStyle.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace NeaStyle.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly NeaStyleContext _context;

    public ProdutosController(NeaStyleContext context)
    {
        _context = context;
    }

    private static ProdutoDto ParaDto(Produto p) => new()
    {
        ProdutoId = p.ProdutoId,
        Nome = p.Nome,
        Tipo = p.Tipo,
        Categoria = p.Categoria,
        Descricao = p.Descricao,
        Ativo = p.Ativo,
        Variacoes = p.ProdutoVariacos.Select(v => new ProdutoVariacaoDto
        {
            ProdutoVariacaoId = v.ProdutoVariacaoId,
            Tamanho = v.Tamanho,
            Cor = v.Cor,
            Estoque = v.Estoque,
            ImagemUrl = v.ImagemUrl,
            Preco = v.Preco
        }).ToList()
    };

    // GET: api/produtos
    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var produtos = await _context.Produtos
            .Include(p => p.ProdutoVariacos)
            .Where(p => p.Ativo)
            .ToListAsync();

        return Ok(produtos.Select(ParaDto));
    }

    // GET: api/produtos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(long id)
    {
        var produto = await _context.Produtos
            .Include(p => p.ProdutoVariacos)
            .FirstOrDefaultAsync(p => p.ProdutoId == id);

        if (produto == null) return NotFound();

        return Ok(ParaDto(produto));
    }

    // POST: api/produtos
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Criar(CreateProdutoDto dto)
    {
        var produto = new Produto
        {
            Nome = dto.Nome,
            Tipo = dto.Tipo,
            Categoria = dto.Categoria,
            Descricao = dto.Descricao,
            Ativo = true
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPorId), new { id = produto.ProdutoId }, ParaDto(produto));
    }

    // PUT: api/produtos/5
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Atualizar(long id, UpdateProdutoDto dto)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        produto.Nome = dto.Nome;
        produto.Tipo = dto.Tipo;
        produto.Categoria = dto.Categoria;
        produto.Descricao = dto.Descricao;
        produto.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/produtos/5  (soft delete — desativa em vez de apagar)
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Desativar(long id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        produto.Ativo = false;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/produtos/5/variacoes
    [HttpPost("{id}/variacoes")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdicionarVariacao(long id, CreateProdutoVariacaoDto dto)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        var variacao = new ProdutoVariaco
        {
            ProdutoId = id,
            Tamanho = dto.Tamanho,
            Cor = dto.Cor,
            Estoque = dto.Estoque,
            EstoqueMinimo = dto.EstoqueMinimo,
            ImagemUrl = dto.ImagemUrl,
            Preco = dto.Preco,
            PrecoCusto = dto.PrecoCusto
        };

        _context.ProdutoVariacoes.Add(variacao);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPorId), new { id }, variacao);
    }
}