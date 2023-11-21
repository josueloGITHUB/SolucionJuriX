using JuriX.Shared;
using System.Net.Http.Json;

namespace JuriX.Client.Services;

public class AbogadoService : IAbogadoService
{
    private readonly HttpClient _http;

    public AbogadoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<AbogadoDTO>> Lista()
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<List<AbogadoDTO>>>("api/abogado/Lista");

        if (result!.EsCorrecto)
            return result.Valor!;
        else
            throw new Exception(result.Mensaje);
    }
    public async Task<AbogadoDTO> Buscar(int id)
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<AbogadoDTO>>($"api/abogado/Buscar/{id}");

        if (result!.EsCorrecto)
            return result.Valor!;
        else
            throw new Exception(result.Mensaje);
    }

    public async Task<int> Guardar(AbogadoDTO abogado)
    {
        var result = await _http.PostAsJsonAsync($"api/abogado/Guardar", abogado);
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.EsCorrecto)
            return response.Valor!;
        else
            throw new Exception(response.Mensaje);
    }
    public async Task<int> Editar(AbogadoDTO abogado)
    {
        var result = await _http.PutAsJsonAsync($"api/abogado/Editar/{abogado.AbogadoId}", abogado);
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.EsCorrecto)
            return response.Valor!;
        else
            throw new Exception(response.Mensaje);
    }
    public async Task<bool> Eliminar(int id)
    {
        var result = await _http.DeleteAsync($"api/abogado/Eliminar/{id}");
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.EsCorrecto)
            return response.EsCorrecto!;
        else
            throw new Exception(response.Mensaje);
    }


}
