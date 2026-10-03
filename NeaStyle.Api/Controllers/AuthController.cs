using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NeaStyle.Api.Data;
using NeaStyle.Api.Models;
using NeaStyle.Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NeaStyle.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly NeaStyleContext _context;
    private readonly IConfiguration _config;
    private readonly PasswordHasher<object> _hasher = new();

    public AuthController(NeaStyleContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    // POST: api/auth/login/cliente
    [HttpPost("login/cliente")]
    public async Task<IActionResult> LoginCliente(LoginRequestDto dto)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Email == dto.Email);
        if (cliente == null) return Unauthorized("Email ou senha inválidos.");

        var resultado = _hasher.VerifyHashedPassword(null!, cliente.Senha, dto.Senha);
        if (resultado == PasswordVerificationResult.Failed) return Unauthorized("Email ou senha inválidos.");

        var token = GerarToken(cliente.UsuarioId, cliente.Nome, "Cliente");
        return Ok(new LoginResponseDto
        {
            Token = token,
            Nome = cliente.Nome,
            Role = "Cliente",
            UsuarioId = cliente.UsuarioId
        });
    }

    // POST: api/auth/login/admin
    [HttpPost("login/admin")]
    public async Task<IActionResult> LoginAdmin(LoginRequestDto dto)
    {
        var admin = await _context.Administradores.FirstOrDefaultAsync(a => a.Email == dto.Email);
        if (admin == null) return Unauthorized("Email ou senha inválidos.");

        var resultado = _hasher.VerifyHashedPassword(null!, admin.Senha, dto.Senha);
        if (resultado == PasswordVerificationResult.Failed) return Unauthorized("Email ou senha inválidos.");

        var token = GerarToken(admin.UsuarioId, admin.Nome, "Admin");
        return Ok(new LoginResponseDto
        {
            Token = token,
            Nome = admin.Nome,
            Role = "Admin",
            UsuarioId = admin.UsuarioId
        });
    }

    // POST: api/auth/registrar
    [HttpPost("registrar")]
    public async Task<IActionResult> RegistrarCliente(RegisterClienteDto dto)
    {
        var existe = await _context.Clientes.AnyAsync(c => c.Email == dto.Email);
        if (existe) return BadRequest("Já existe um cliente com esse email.");

        var cliente = new Cliente
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Cpf = dto.CPF,
            Endereco = dto.Endereco,
            Telefone = dto.Telefone,
            DataNascimento = dto.DataNascimento
        };
        cliente.Senha = _hasher.HashPassword(null!, dto.Senha);

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        return Ok(new { mensagem = "Cliente cadastrado com sucesso.", cliente.UsuarioId });
    }

    private string GerarToken(long usuarioId, string nome, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuarioId.ToString()),
            new(ClaimTypes.Name, nome),
            new(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiraEmMinutos"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}