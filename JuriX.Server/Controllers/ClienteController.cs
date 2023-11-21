using JuriX.Server.Models;
using JuriX.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuriX.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly JurixContext _dbContext;

        public ClienteController(JurixContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseApi<List<ClienteDTO>>();
            var listaClienteDTO = new List<ClienteDTO>();

            try
            {
                foreach (var item in await _dbContext.Clientes.ToListAsync())
                {
                    listaClienteDTO.Add(new ClienteDTO
                    {
                        ClienteId = item.ClienteId,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaClienteDTO;
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
            var responseApi = new ResponseApi<ClienteDTO>();
            var ClienteDTO = new ClienteDTO();

            try
            {
                var dbCliente = await _dbContext.Clientes.FirstOrDefaultAsync(x => x.ClienteId == id);

                if (dbCliente != null)
                {
                    ClienteDTO.ClienteId = dbCliente.ClienteId;
                    ClienteDTO.Nombre = dbCliente.Nombre;
                    ClienteDTO.Descripcion = dbCliente.Descripcion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ClienteDTO;
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
        public async Task<IActionResult> Guardar(ClienteDTO cliente)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbCliente = new Cliente
                {
                    Nombre = cliente.Nombre,
                    Descripcion = cliente.Descripcion
                };

                _dbContext.Clientes.Add(dbCliente);
                await _dbContext.SaveChangesAsync();

                if (dbCliente.ClienteId != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbCliente.ClienteId;
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
        public async Task<IActionResult> Editar(ClienteDTO cliente, int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {

                var dbCliente = await _dbContext.Clientes.FirstOrDefaultAsync(e => e.ClienteId == id);


                if (dbCliente != null)
                {
                    dbCliente.Nombre = cliente.Nombre;
                    dbCliente.Descripcion = cliente.Descripcion;

                    _dbContext.Clientes.Update(dbCliente);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbCliente.ClienteId;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "cliente no encontrado";
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
