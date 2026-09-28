using System;
using System.Collections.Generic;

namespace NeaStyle.Api.Models;

public partial class Administradore
{
    public long UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Cargo { get; set; } = null!;
}
