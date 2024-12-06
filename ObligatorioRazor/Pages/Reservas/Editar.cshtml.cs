using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioRazor.Pages.Reservas
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using ObligatorioRazor.Data;
    using ObligatorioRazor.Model;
    using static System.Net.Mime.MediaTypeNames;

    public class EditarModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;

        public EditarModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public Reserva Reserva { get; set; }

        public IEnumerable<SelectListItem> HabitacionesDisponibles { get; set; }

        public List<SelectListItem> UsuariosDisponibles { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Cargar la reserva
            Reserva = await _contexto.Reservas.FindAsync(id);

            if (Reserva == null)
            {
                return NotFound();
            }

            // Llamada al método para cargar las listas de habitaciones y usuarios
            await CargarListas();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validación de fechas
            if (Reserva.FechaInicio >= Reserva.FechaFin)
            {
                ModelState.AddModelError(string.Empty, "La fecha de inicio debe ser anterior a la fecha de fin.");
                await CargarListas(); // Recargar las listas
                return Page();
            }

            // Validación de duración
            var duracionReserva = (Reserva.FechaFin - Reserva.FechaInicio).Days;
            if (duracionReserva > 30)
            {
                ModelState.AddModelError(string.Empty, "No se puede realizar una reserva por más de 30 días.");
                await CargarListas(); // Recargar las listas
                return Page();
            }

            // Buscar la reserva existente
            var reservaExistente = await _contexto.Reservas
                .FirstOrDefaultAsync(r => r.IDReserva == Reserva.IDReserva);

            if (reservaExistente == null)
            {
                ModelState.AddModelError(string.Empty, "La reserva no existe.");
                return NotFound();
            }

            // Verificar si la habitación está disponible en el rango de fechas
            var reservasActivas = await _contexto.Reservas
                .Where(r => r.HabitacionId == Reserva.HabitacionId &&
                           r.FechaInicio < Reserva.FechaFin &&
                           r.FechaFin > Reserva.FechaInicio)
                .ToListAsync();

            if (reservasActivas.Any())
            {
                ModelState.AddModelError(string.Empty, "La habitación seleccionada no está disponible.");
                await CargarListas(); // Recargar las listas
                return Page();
            }

            // Actualizar la reserva
            reservaExistente.FechaInicio = Reserva.FechaInicio;
            reservaExistente.FechaFin = Reserva.FechaFin;
            reservaExistente.HabitacionId = Reserva.HabitacionId;
            reservaExistente.UsuarioId = Reserva.UsuarioId;

            // Guardar los cambios
            _contexto.Update(reservaExistente);
            await _contexto.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private async Task CargarListas()
        {
            // Obtener las habitaciones disponibles
            var reservasActivas = await _contexto.Reservas
                .Where(r => r.FechaInicio < Reserva.FechaFin && Reserva.FechaInicio < r.FechaFin)
                .Select(r => r.HabitacionId)
                .ToListAsync();

            HabitacionesDisponibles = await _contexto.Habitaciones
                .Where(h => !reservasActivas.Contains(h.HabitacionId)) // Excluir habitaciones ya reservadas
                .Select(h => new SelectListItem
                {
                    Value = h.HabitacionId.ToString(),
                    Text = $"N° {h.HabitacionId} - {h.Tipo} - ${h.Precio}"
                })
                .ToListAsync();

            // Obtener los usuarios disponibles
            UsuariosDisponibles = await _contexto.Usuarios
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Id} - {u.Nombre}"
                })
                .ToListAsync();
        }
    }
}