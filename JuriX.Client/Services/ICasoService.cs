using JuriX.Shared;

namespace JuriX.Client.Services
{
    public interface ICasoService
    {
        Task<List<CasoDTO>> Lista();
        Task<CasoDTO> Buscar(int id);

        Task<int> Guardar(CasoDTO caso);
        Task<bool> Eliminar(int id);
        Task<int> Editar(CasoDTO caso);
    }
}
