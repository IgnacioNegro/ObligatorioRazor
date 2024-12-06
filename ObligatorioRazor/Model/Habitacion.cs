using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioRazor.Model
{
    [Table("Habitacion")]
    public class Habitacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HabitacionId { get; set; }

        [Required]
        public string? Tipo { get; set; }

        [Required]
        public int Precio { get; set; }

        [Required]
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}



