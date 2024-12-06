using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ObligatorioRazor.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; } = "Hello, Razor Pages!";

        public void OnGet()
        {
            // Lógica para manejar solicitudes GET
        }
    }
}
