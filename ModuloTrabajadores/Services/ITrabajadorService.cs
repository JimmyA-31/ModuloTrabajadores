using ModuloTrabajadores.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModuloTrabajadores.Services
{
    public interface ITrabajadorService
    {
        Task<IEnumerable<Trabajador>> ListarTrabajadoresAsync(string? sexo);
        Task<Trabajador?> ObtenerPorIdAsync(int id);
        Task RegistrarTrabajadorAsync(Trabajador trabajador);
        Task EditarTrabajadorAsync(Trabajador trabajador);
        Task EliminarTrabajadorAsync(int id);
        Task<IEnumerable<Trabajador>> BuscarAsync(string texto);
    }
}
