using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuriX.Shared;

public class ClienteDTO
{
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string? Descripcion { get; set; }
}
