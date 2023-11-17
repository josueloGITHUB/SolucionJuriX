using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuriX.Shared;

public class AbogadoDTO
{
    public int AbogadoId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string? Especialidad { get; set; }
    [Required]
    [Range(1, int.MaxValue,ErrorMessage = "El campo {0} es requerido.")]
    public int? DespachoId { get; set; }

    public DespachoDTO? Despacho { get; set; }

}
