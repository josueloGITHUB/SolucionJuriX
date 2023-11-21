using JuriX.Shared;
using System.Net.Http.Json;

namespace JuriX.Client.Services;

public class DespachoService : IDespachoService
{
    private readonly HttpClient _http;
    public DespachoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<DespachoDTO>> Lista()
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<List<DespachoDTO>>>("api/despacho/Lista");

        if (result!.EsCorrecto)
            return result.Valor!;
        else
            throw new Exception(result.Mensaje);

    }
    public async Task<DespachoDTO> Buscar(int id)
    {
        var result = await _http.GetFromJsonAsync<ResponseApi<DespachoDTO>>($"api/despacho/Buscar/{id}");

        if (result!.EsCorrecto)
            return result.Valor!;
        else
            throw new Exception(result.Mensaje);
    }
    public async Task<int> Guardar(DespachoDTO despacho)
    {
        var result = await _http.PostAsJsonAsync($"api/despacho/Guardar", despacho);
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.EsCorrecto)
            return response.Valor!;
        else
            throw new Exception(response.Mensaje);
    }

    public async Task<int> Editar(DespachoDTO despacho)
    {
        var result = await _http.PutAsJsonAsync($"api/despacho/Editar/{despacho.DespachoId}", despacho);
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.EsCorrecto)
            return response.Valor!;
        else
            throw new Exception(response.Mensaje);
    }

    public async Task<bool> Eliminar(int id)
    {
        var result = await _http.DeleteAsync($"api/despacho/Eliminar/{id}");
        var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

        if (response!.EsCorrecto)
            return response.EsCorrecto!;
        else
            throw new Exception(response.Mensaje);
    }


}
