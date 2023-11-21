using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using JuriX.Server.Models;
using JuriX.Shared;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.CompilerServices;

namespace JuriX.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasoController : ControllerBase
    {
        private readonly JurixContext _dbContext;

        public CasoController(JurixContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseApi<List<CasoDTO>>();
            var listaCasoDTO = new List<CasoDTO>();

            try
            {
                foreach (var item in await _dbContext.Casos.Include(a => a.AbogadoAsignado).Include(c => c.Cliente).ToListAsync())
                {
                    listaCasoDTO.Add(new CasoDTO
                    {
                        CasoId = item.CasoId,
                        Fecha = item.Fecha,
                        TipoCaso = item.TipoCaso,
                        Descripcion = item.Descripcion,
                        Estado = item.Estado,
                        Cliente = new ClienteDTO
                        {
                            ClienteId = item.Cliente.ClienteId,
                            Nombre = item.Cliente.Nombre,
                            Descripcion = item.Cliente.Descripcion
                        },
                        AbogadoAsignado = new AbogadoDTO
                        {
                            AbogadoId = item.AbogadoAsignado.AbogadoId,
                            Nombre = item.AbogadoAsignado.Nombre,
                            Especialidad = item.AbogadoAsignado.Especialidad,
                            DespachoId = item.AbogadoAsignado.DespachoId

                        }
                        
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaCasoDTO;
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
            var responseApi = new ResponseApi<CasoDTO>();
            var CasoDTO = new CasoDTO();

            try
            {
                var dbCaso = await _dbContext.Casos.FirstOrDefaultAsync(x => x.CasoId == id);

                if (dbCaso != null)
                {
                    CasoDTO.CasoId = dbCaso.CasoId;
                    CasoDTO.Fecha = dbCaso.Fecha;
                    CasoDTO.TipoCaso = dbCaso.TipoCaso;
                    CasoDTO.Descripcion = dbCaso.Descripcion;
                    CasoDTO.Estado = dbCaso.Estado;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = CasoDTO;
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

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(CasoDTO caso)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbCaso = new Caso
                {
                    Fecha = caso.Fecha,
                    ClienteId = caso.ClienteId,
                    TipoCaso = caso.TipoCaso,
                    Descripcion = caso.Descripcion,
                    AbogadoAsignadoId = caso.AbogadoAsignadoId,
                    Estado = caso.Estado
                };

                _dbContext.Casos.Add(dbCaso);
                await _dbContext.SaveChangesAsync();

                if (dbCaso.CasoId != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbCaso.CasoId;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(CasoDTO caso, int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {

                var dbCaso = await _dbContext.Casos.FirstOrDefaultAsync(e => e.CasoId == id);


                if (dbCaso != null)
                {
                    dbCaso.Fecha = caso.Fecha;
                    dbCaso.ClienteId = caso.ClienteId;
                    dbCaso.TipoCaso = caso.TipoCaso;
                    dbCaso.Descripcion = caso.Descripcion;
                    dbCaso.AbogadoAsignadoId = caso.AbogadoAsignadoId;
                    dbCaso.Estado = caso.Estado;

                    _dbContext.Casos.Update(dbCaso);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbCaso.CasoId;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Caso no encontrado";
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {

                var dbCliente = await _dbContext.Clientes.FirstOrDefaultAsync(e => e.ClienteId == id);


                if (dbCliente != null)
                {

                    _dbContext.Clientes.Remove(dbCliente);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Abogado no encontrado";
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
