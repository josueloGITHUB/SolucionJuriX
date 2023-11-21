using JuriX.Shared;
using System.Net.Http.Json;

namespace JuriX.Client.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _http;

        public ClienteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ClienteDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<ClienteDTO>>>("api/cliente/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<ClienteDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<ClienteDTO>>($"api/cliente/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }
        public async Task<int> Guardar(ClienteDTO cliente)
        {
            var result = await _http.PostAsJsonAsync($"api/cliente/Guardar", cliente);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(ClienteDTO cliente)
        {
            var result = await _http.PutAsJsonAsync($"api/cliente/Editar/{cliente.ClienteId}", cliente);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/cliente/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }




    }
}
