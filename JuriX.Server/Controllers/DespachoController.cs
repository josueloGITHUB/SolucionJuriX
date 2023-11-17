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
}

