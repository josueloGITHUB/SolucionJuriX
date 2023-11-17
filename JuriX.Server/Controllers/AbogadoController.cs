using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using JuriX.Server.Models;
using JuriX.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JuriX.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbogadoController : ControllerBase
    {
        private readonly JurixContext _dbContext;

        public AbogadoController(JurixContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseApi<List<AbogadoDTO>>();
            var listaAbogadoDTO = new List<AbogadoDTO>();

            try
            {
                foreach (var item in await _dbContext.Abogados.Include(d => d.Despacho).ToListAsync())
                {
                    listaAbogadoDTO.Add(new AbogadoDTO
                    {
                        AbogadoId = item.AbogadoId,
                        Nombre = item.Nombre,
                        Especialidad = item.Especialidad,
                        Despacho = new DespachoDTO
                        {
                            DespachoId = item.Despacho.DespachoId,
                            Nombre = item.Despacho.Nombre,
                            Direccion = item.Despacho.Direccion
                        }
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaAbogadoDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseApi<AbogadoDTO>();
            var AbogadoDTO = new AbogadoDTO();

            try
            {
                var dbAbogado = await _dbContext.Abogados.FirstOrDefaultAsync(x => x.AbogadoId == id);
                
                if(dbAbogado != null)
                {
                    AbogadoDTO.AbogadoId = dbAbogado.AbogadoId;
                    AbogadoDTO.Nombre = dbAbogado.Nombre;
                    AbogadoDTO.Especialidad = dbAbogado.Especialidad;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = AbogadoDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }


            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
    }

}
