using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using JuriX.Server.Models;
using JuriX.Shared;
using Microsoft.EntityFrameworkCore;

namespace JuriX.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DespachoController : ControllerBase
{
    private readonly JurixContext _dbContext;

    public DespachoController(JurixContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    [Route("Lista")]
    public async Task<IActionResult> Lista()
    {
        var responseApi = new ResponseApi<List<DespachoDTO>>();
        var listaDespachoDTO = new List<DespachoDTO>();

        try
        {
            foreach(var item in await _dbContext.Despachos.ToListAsync())
            {
                listaDespachoDTO.Add(new DespachoDTO
                {
                    DespachoId = item.DespachoId,
                    Nombre = item.Nombre,
                    Direccion = item.Direccion
                });
            }
            responseApi.EsCorrecto = true;
            responseApi.Valor = listaDespachoDTO;
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
        var responseApi = new ResponseApi<DespachoDTO>();
        var DespachoDTO = new DespachoDTO();

        try
        {
            var dbDespacho = await _dbContext.Despachos.FirstOrDefaultAsync(x => x.DespachoId == id);

            if (dbDespacho != null)
            {
                DespachoDTO.DespachoId = dbDespacho.DespachoId;
                DespachoDTO.Nombre = dbDespacho.Nombre;
                DespachoDTO.Direccion = dbDespacho.Direccion;

                responseApi.EsCorrecto = true;
                responseApi.Valor = DespachoDTO;
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
            responseApi.Mensaje = ex.Message; ;
        }
        return Ok(responseApi);
    }

    [HttpPost]
    [Route("Guardar")]
    public async Task<IActionResult> Guardar(DespachoDTO abogado)
    {
        var responseApi = new ResponseApi<int>();

        try
        {
            var dbDespacho = new Despacho
            {
                Nombre = abogado.Nombre,
                Direccion = abogado.Direccion
            };

            _dbContext.Despachos.Add(dbDespacho);
            await _dbContext.SaveChangesAsync();

            if (dbDespacho.DespachoId != 0)
            {
                responseApi.EsCorrecto = true;
                responseApi.Valor = dbDespacho.DespachoId;
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
    public async Task<IActionResult> Editar(DespachoDTO despacho, int id)
    {
        var responseApi = new ResponseApi<int>();

        try
        {

            var dbDespacho = await _dbContext.Despachos.FirstOrDefaultAsync(e => e.DespachoId == id);


            if (dbDespacho != null)
            {
                dbDespacho.Nombre = despacho.Nombre;
                dbDespacho.Direccion = despacho.Direccion;

                 _dbContext.Despachos.Update(dbDespacho);
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbDespacho.DespachoId;
            }
            else
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Despacho no encontrado";
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

            var dbDespacho = await _dbContext.Despachos.FirstOrDefaultAsync(e => e.DespachoId == id);


            if (dbDespacho != null)
            {

                _dbContext.Despachos.Remove(dbDespacho);
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

