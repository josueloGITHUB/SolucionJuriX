using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using JuriX.Shared;

namespace JuriX.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            SesionDTO sesionDTO = new SesionDTO();
            if(login.Correo == "admin@jurix.com" && login.Clave == "admin")
            {
                sesionDTO.Nombre = "admin";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Administrador";
            }
            else
            {
                sesionDTO.Nombre = "abogado";
                sesionDTO.Correo = login.Correo;
                sesionDTO.Rol = "Abogado";
            }
            return StatusCode(StatusCodes.Status200OK, sesionDTO);
        }
    }
}
