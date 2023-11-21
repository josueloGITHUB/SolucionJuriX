using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuriX.Shared;

public class CasoDTO
{
    public int CasoId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "El campo Cliente es requerido")]
    [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
    public int? ClienteId { get; set; }
    [Required(ErrorMessage = "El campo Tipo de Caso es requerido")]
    public string? TipoCaso { get; set; }

    public string? Descripcion { get; set; }
    [Required(ErrorMessage = "El campo Abogado Asignado es requerido")]
    [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
    public int? AbogadoAsignadoId { get; set; }
    [Required(ErrorMessage = "El campo Estado del caso Asignado es requerido")]
    public string? Estado { get; set; }

    public AbogadoDTO? AbogadoAsignado { get; set; }

    public ClienteDTO? Cliente { get; set; }
}
