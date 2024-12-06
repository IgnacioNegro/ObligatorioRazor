using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ObligatorioRazor.Data;
using ObligatorioRazor.Model;

namespace ObligatorioRazor.Pages.Reservas
{
    public class CrearModel : PageModel
    {
        private readonly ApplicationDbContext _contexto;

        public CrearModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [BindProperty]
        public Reserva Reserva { get; set; } = new Reserva();

        [BindProperty]
        public List<SelectListItem> HabitacionesDisponibles { get; set; }
        [BindProperty]
        public List<SelectListItem> UsuariosDisponibles { get; set; }

        public async Task OnGetAsync()
        {
            if (Reserva.FechaInicio == default(DateTime))
            {
                Reserva.FechaInicio = DateTime.Today;
            }

            if (Reserva.FechaFin == default(DateTime))
            {
                Reserva.FechaFin = DateTime.Today;
            }

            await CargarListas(); // Reutiliza el m�todo para cargar listas
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validaci�n de fechas
            if (Reserva.FechaInicio >= Reserva.FechaFin)
            {
                ModelState.AddModelError(string.Empty, "La fecha de inicio debe ser anterior a la fecha de fin.");
                await CargarListas();
                return Page();
            }

            // Validaci�n de duraci�n
            var duracionReserva = (Reserva.FechaFin - Reserva.FechaInicio).Days;
            if (duracionReserva > 30)
            {
                ModelState.AddModelError(string.Empty, "No se puede realizar una reserva por m�s de 30 d�as.");
                await CargarListas();
                return Page();
            }

            // Verificar si la habitaci�n est� reservada en el rango de fechas seleccionado
            var reservasActivas = await _contexto.Reservas
                .Where(r => r.HabitacionId == Reserva.HabitacionId &&
                           ((Reserva.FechaInicio < r.FechaFin && Reserva.FechaFin > r.FechaInicio) ||
                            (Reserva.FechaInicio < r.FechaFin && Reserva.FechaFin > r.FechaInicio)))
                .ToListAsync();

            if (reservasActivas.Any())
            {
                ModelState.AddModelError(string.Empty, "La habitaci�n seleccionada no est� disponible.");
                await CargarListas();
                return Page();
            }

            // Si todo es v�lido, HACER la REServa
            _contexto.Reservas.Add(Reserva);
            await _contexto.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task CargarListas()
        {
            
            var reservasActivas = await _contexto.Reservas
                .Where(r => (Reserva.FechaInicio < r.FechaFin && Reserva.FechaFin > r.FechaInicio))
                .Select(r => r.HabitacionId)
                .ToListAsync();

            HabitacionesDisponibles = await _contexto.Habitaciones
                .Where(h => !reservasActivas.Contains(h.HabitacionId))  
                .Select(h => new SelectListItem
                {
                    Value = h.HabitacionId.ToString(),
                    Text = $"N� {h.HabitacionId} - {h.Tipo} - ${h.Precio}"
                })
                .ToListAsync();

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
