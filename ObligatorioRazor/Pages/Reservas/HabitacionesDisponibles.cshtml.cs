using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioRazor.Data;
using ObligatorioRazor.Model;

namespace ObligatorioRazor.Pages.Reservas
{
    public class HabitacionesDisponiblesModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public HabitacionesDisponiblesModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaSalida { get; set; }
        public List<Habitacion> Habitaciones { get; set; }

        public async Task OnGetAsync(DateTime? fechaInicio, DateTime? fechaSalida)
        {
            FechaInicio = fechaInicio;
            FechaSalida = fechaSalida;

            if (FechaInicio.HasValue && FechaSalida.HasValue)
            {
                // Filtrar habitaciones que no tienen reservas solapadas
                Habitaciones = await _context.Habitaciones
                    .Where(h => !_context.Reservas
                        .Any(r => r.HabitacionId == h.HabitacionId &&
                                  r.FechaInicio < FechaSalida.Value &&
                                  r.FechaFin > FechaInicio.Value))
                    .ToListAsync();
            }
            else
            {
                TempData["Mensaje"] = "Seleccione una fecha para filtrar las habitaciones";
            }
        }
    }
}