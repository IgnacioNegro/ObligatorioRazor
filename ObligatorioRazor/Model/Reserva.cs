using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioRazor.Model
{
    [Table("Reserva")]
    public class Reserva
    {
        [Key]
        [Column("ReservaID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDReserva { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",
            ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? EmailCliente { get;  set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [DataType(DataType.Date)]
        public DateTime FechaReserva { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El campo es obligatorio.")]
        public bool EstaPagada { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        
        
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio.")]
        [ForeignKey("HabitacionId")]
        public int HabitacionId { get; set; }
        public Habitacion? Habitacion { get; set; }
    }
}
