using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioRazor.Model
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("UsuarioID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } 
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}



