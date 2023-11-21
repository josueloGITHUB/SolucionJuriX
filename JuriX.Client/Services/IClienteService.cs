using JuriX.Shared;

namespace JuriX.Client.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> Lista();
        Task<ClienteDTO> Buscar(int id);

        Task<int> Guardar(ClienteDTO cliente);
        Task<bool> Eliminar(int id);
        Task<int> Editar(ClienteDTO cliente);
    }
}
