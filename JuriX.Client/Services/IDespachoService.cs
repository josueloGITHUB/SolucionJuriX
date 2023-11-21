using JuriX.Shared;


namespace JuriX.Client.Services;

public interface IDespachoService
{
    Task<List<DespachoDTO>> Lista();
    Task<DespachoDTO> Buscar(int id);
    Task<int> Guardar(DespachoDTO despacho);
    Task<bool> Eliminar(int id);
    Task<int> Editar(DespachoDTO despacho);

}
