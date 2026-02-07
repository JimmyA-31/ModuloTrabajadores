using Microsoft.EntityFrameworkCore;
using ModuloTrabajadores.Data;
using ModuloTrabajadores.Models;
using MySqlConnector;
using System.Data;

namespace ModuloTrabajadores.Services
{
    public class TrabajadorService : ITrabajadorService
    {

        private readonly ApplicationDbContext _context;

        public TrabajadorService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= LISTAR =================
        public async Task<IEnumerable<Trabajador>> ListarTrabajadoresAsync(string? sexo)
        {
            // Usamos EF Core normal para el listado
            var query = _context.Trabajadores.AsQueryable();
            if (!string.IsNullOrEmpty(sexo))
                query = query.Where(t => t.Sexo == sexo);

            return await query.ToListAsync();
        }

        // ================= OBTENER POR ID =================
        public async Task<Trabajador?> ObtenerPorIdAsync(int id)
        {
            return await _context.Trabajadores
                                 .FromSqlRaw("CALL sp_ObtenerTrabajadorPorId({0})", id)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync();
        }

        // ================= REGISTRAR =================
        public async Task RegistrarTrabajadorAsync(Trabajador trabajador)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "CALL sp_RegistrarTrabajador({0},{1},{2},{3},{4},{5},{6},{7})",
                trabajador.Nombres,
                trabajador.Apellidos,
                trabajador.TipoDocumento,
                trabajador.NumeroDocumento,
                trabajador.Sexo,
                trabajador.FechaNacimiento,
                trabajador.Direccion,
                trabajador.Foto
            );
        }

        // ================= EDITAR =================
        public async Task EditarTrabajadorAsync(Trabajador trabajador)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "CALL sp_EditarTrabajador({0},{1},{2},{3},{4},{5},{6},{7},{8})",
                trabajador.Id,
                trabajador.Nombres,
                trabajador.Apellidos,
                trabajador.TipoDocumento,
                trabajador.NumeroDocumento,
                trabajador.Sexo,
                trabajador.FechaNacimiento,
                trabajador.Direccion,
                trabajador.Foto
            );
        }

        // ================= ELIMINAR =================  
        public async Task EliminarTrabajadorAsync(int id)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "CALL sp_EliminarTrabajador({0})",
                id
            );
        }

        // ================= BUSCAR =================
        public async Task<IEnumerable<Trabajador>> BuscarAsync(string texto)
        {
            return await _context.Trabajadores
                                 .FromSqlRaw("CALL sp_BuscarTrabajadores({0})", texto)
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
    
}
