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

                });
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}

