using System;
using System.Collections.Generic;
using System.Text;

namespace NeaStyle.Shared.Dtos;

public class LoginRequestDto
{
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
}

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string Role { get; set; } = null!;
    public long UsuarioId { get; set; }
}

public class RegisterClienteDto
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string? CPF { get; set; }
    public string? Endereco { get; set; }
    public string? Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
}
