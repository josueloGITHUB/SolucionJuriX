using System;
using System.Collections.Generic;

namespace JuriX.Server.Models;

public partial class Despacho
{
    public int DespachoId { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Abogado> Abogados { get; set; } = new List<Abogado>();
}
