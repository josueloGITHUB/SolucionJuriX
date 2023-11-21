using JuriX.Shared;

namespace JuriX.Client.Services
{
    public interface IAbogadoService
    {
        Task<List<AbogadoDTO>> Lista();
        Task<AbogadoDTO> Buscar(int id);

        Task<int> Guardar(AbogadoDTO abogado);
        Task<bool> Eliminar(int id);
        Task<int> Editar(AbogadoDTO abogado);
    }
}
