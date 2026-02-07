using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloTrabajadores.Data;
using ModuloTrabajadores.Models;
using ModuloTrabajadores.Services;

namespace ModuloTrabajadores.Controllers
{
    public class TrabajadoresController : Controller
    {

        private readonly ITrabajadorService _trabajadorService;
        private readonly IWebHostEnvironment _environment;

        public TrabajadoresController(
            ITrabajadorService trabajadorService,
            IWebHostEnvironment environment)
        {
            _trabajadorService = trabajadorService;
            _environment = environment;
        }

        // ================= LISTADO + FILTRO =================
        public async Task<IActionResult> Index(string? sexo)
        {
            ViewBag.Sexo = sexo;
            var lista = await _trabajadorService.ListarTrabajadoresAsync(sexo);
            return View(lista);
        }

        // ================= CREATE (GET) =================
        public IActionResult Create()
        {
            return View();
        }

        // ================= CREATE (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trabajador trabajador)
        {
            if (!ModelState.IsValid)
                return View(trabajador);


            if (trabajador.FotoArchivo != null && trabajador.FotoArchivo.Length > 0)
            {
                string folder = Path.Combine(_environment.WebRootPath, "fotos");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(trabajador.FotoArchivo.FileName);
                string path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await trabajador.FotoArchivo.CopyToAsync(stream);

                trabajador.Foto = fileName;
            }
            else
            {
                trabajador.Foto = "default.png";
            }

            try
            {
                await _trabajadorService.RegistrarTrabajadorAsync(trabajador);
                TempData["Success"] = "Trabajador registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error al registrar el trabajador.";
                return View(trabajador);
            }
        }

        // ================= EDIT (GET) =================
        public async Task<IActionResult> Edit(int id)
        {
            var trabajador = await _trabajadorService.ObtenerPorIdAsync(id);
            if (trabajador == null)
                return NotFound();

            return View(trabajador);
        }

        // ================= EDIT (POST) =================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trabajador trabajador)
        {
            if (id != trabajador.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(trabajador);

            if (trabajador.FotoArchivo != null && trabajador.FotoArchivo.Length > 0)
            {
                string folder = Path.Combine(_environment.WebRootPath, "fotos");
                Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + Path.GetExtension(trabajador.FotoArchivo.FileName);
                string path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await trabajador.FotoArchivo.CopyToAsync(stream);

                trabajador.Foto = fileName;
            }

            try 
            { 
            await _trabajadorService.EditarTrabajadorAsync(trabajador);

            TempData["Success"] = "Datos del trabajador actualizados.";
            return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error al actualizar el trabajador.";
                return View(trabajador);
            }
}

        // ================= DETAILS =================
        public async Task<IActionResult> Details(int id)
        {
            var trabajador = await _trabajadorService.ObtenerPorIdAsync(id);
            if (trabajador == null)
                return NotFound();

            return View(trabajador);
        }

        // ================= DELETE (GET) =================
        public async Task<IActionResult> Delete(int id)
        {
            var trabajador = await _trabajadorService.ObtenerPorIdAsync(id);
            if (trabajador == null)
                return NotFound();

            return View(trabajador);
        }

        // ================= DELETE (POST) =================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _trabajadorService.EliminarTrabajadorAsync(id);
                TempData["Success"] = "Trabajador eliminado correctamente.";
            }
            catch
            {
                TempData["Error"] = "No se pudo eliminar el trabajador.";
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
