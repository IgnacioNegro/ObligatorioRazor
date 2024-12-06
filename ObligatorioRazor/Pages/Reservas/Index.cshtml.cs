using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ObligatorioRazor.Data;
using ObligatorioRazor.Model;

namespace ObligatorioRazor.Pages.Reservas
{
    public class ReservasModel : PageModel
    {
        public IEnumerable<Reserva> Reservas { get; set; }

        private readonly ApplicationDbContext _context;

        public ReservasModel(ApplicationDbContext context)
        { _context = context; }


        public async Task OnGetAsync()
        {
            Reservas = await _context.Reservas
                .Include(r => r.Habitacion)
                .ToListAsync();

            Reservas = _context.Reservas
           .Include(r => r.Usuario) 
           .ToList();
        }



        public async Task<IActionResult> OnPostBorrar(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}

