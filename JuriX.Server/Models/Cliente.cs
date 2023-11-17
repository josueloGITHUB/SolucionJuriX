using System;
using System.Collections.Generic;

namespace JuriX.Server.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Caso> Casos { get; set; } = new List<Caso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
