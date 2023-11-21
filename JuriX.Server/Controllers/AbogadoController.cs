﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(AbogadoDTO abogado)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbAbogado = new Abogado
                {
                    Nombre = abogado.Nombre,
                    Especialidad = abogado.Especialidad,
                    DespachoId = abogado.DespachoId
                };

                _dbContext.Abogados.Add(dbAbogado);
                await _dbContext.SaveChangesAsync();

                if (dbAbogado.AbogadoId != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbAbogado.AbogadoId;
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
        public async Task<IActionResult> Editar(AbogadoDTO abogado, int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {

                var dbAbogado = await _dbContext.Abogados.FirstOrDefaultAsync(e => e.AbogadoId == id);


                if (dbAbogado != null)
                {
                    dbAbogado.Nombre = abogado.Nombre;
                    dbAbogado.Especialidad = abogado.Especialidad;
                    dbAbogado.DespachoId = abogado.DespachoId; 

                    _dbContext.Abogados.Update(dbAbogado);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbAbogado.AbogadoId;
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

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {

                var dbAbogado = await _dbContext.Abogados.FirstOrDefaultAsync(e => e.AbogadoId == id);


                if (dbAbogado != null)
                {

                    _dbContext.Abogados.Remove(dbAbogado);
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
