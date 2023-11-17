using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuriX.Shared;

public class CasoDTO
{
    public int CasoId { get; set; }

    public DateTime? Fecha { get; set; }

    public int? ClienteId { get; set; }

    public string? TipoCaso { get; set; }

    public string? Descripcion { get; set; }

    public int? AbogadoAsignadoId { get; set; }

    public string? Estado { get; set; }
}
