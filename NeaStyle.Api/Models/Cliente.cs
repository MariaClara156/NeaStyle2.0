using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class Cliente
{
    public long UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Endereco { get; set; } = null!;

    public string Telefone { get; set; } = null!;

    public DateTime DataNascimento { get; set; }

    public string Cpf { get; set; } = null!;

    public virtual ICollection<Carrinho> Carrinhos { get; set; } = new List<Carrinho>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<Pedido> PedidoClienteUsuarios { get; set; } = new List<Pedido>();

    public virtual ICollection<Pedido> PedidoClientes { get; set; } = new List<Pedido>();
}
