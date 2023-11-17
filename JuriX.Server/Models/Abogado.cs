using System;
using System.Collections.Generic;

namespace JuriX.Server.Models;

public partial class Abogado
{
    public int AbogadoId { get; set; }

    public string? Nombre { get; set; }

    public string? Especialidad { get; set; }

    public int? DespachoId { get; set; }

    public virtual ICollection<Caso> Casos { get; set; } = new List<Caso>();

    public virtual Despacho? Despacho { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
