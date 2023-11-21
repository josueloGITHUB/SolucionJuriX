using System;
using System.Collections.Generic;

namespace JuriX.Server.Models;

public partial class Caso
{
    public int CasoId { get; set; }

    public DateTime Fecha { get; set; }

    public int? ClienteId { get; set; }

    public string? TipoCaso { get; set; }

    public string? Descripcion { get; set; }

    public int? AbogadoAsignadoId { get; set; }

    public string? Estado { get; set; }

    public virtual Abogado? AbogadoAsignado { get; set; }

    public virtual Cliente? Cliente { get; set; }
}
