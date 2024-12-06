using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioRazor.Data;
using ObligatorioRazor.Model;

namespace ObligatorioRazor.Pages.Reservas
{
    public class EstadisticasModel : PageModel
    {
        public readonly ApplicationDbContext _contexto;

        public EstadisticasModel(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public List<Habitacion> HabitacionesMasReservadas { get; set; }
        public List<Usuario> UsuariosMasReservaron { get; set; }
        public void OnGet()
        {
            
          HabitacionesMasReservadas = _contexto.Habitaciones
                .Where(h => _contexto.Reservas.Count(r => r.HabitacionId == h.HabitacionId) > 1)
                .OrderByDescending(h => _contexto.Reservas.Count(r => r.HabitacionId == h.HabitacionId))
                .ToList();

            
         UsuariosMasReservaron = _contexto.Usuarios
                .Where(u => _contexto.Reservas.Count(r => r.UsuarioId == u.Id) > 1)
                .OrderByDescending(u => _contexto.Reservas.Count(r => r.UsuarioId == u.Id))
                .ToList();
        }

    }
}
