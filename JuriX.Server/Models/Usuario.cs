using System;
using System.Collections.Generic;

namespace JuriX.Server.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Contrasena { get; set; }

    public string? Rol { get; set; }

    public int? AbogadoId { get; set; }

    public int? ClienteId { get; set; }

    public virtual Abogado? Abogado { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
