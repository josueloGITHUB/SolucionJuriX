using JuriX.Shared;
using System.Net.Http.Json;

namespace JuriX.Client.Services
{
    public class CasoService : ICasoService
    {
        private readonly HttpClient _http;

        public CasoService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<CasoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<CasoDTO>>>("api/caso/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<CasoDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<CasoDTO>>($"api/caso/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(CasoDTO caso)
        {
            var result = await _http.PostAsJsonAsync($"api/caso/Guardar", caso);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<int> Editar(CasoDTO caso)
        {
            var result = await _http.PutAsJsonAsync($"api/caso/Editar/{caso.CasoId}", caso);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }
        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/caso/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
