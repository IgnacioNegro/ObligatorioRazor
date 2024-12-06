using Microsoft.EntityFrameworkCore;
using ObligatorioRazor.Model;

namespace ObligatorioRazor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Reserva>? Reservas { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Habitacion>? Habitaciones { get; set; }

        //Precarga de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación entre Reserva y Usuario (uno a muchos)
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)    // Una Reserva tiene un Usuario
                .WithMany(u => u.Reservas) // Un Usuario puede tener muchas Reservas
                .HasForeignKey(r => r.UsuarioId)  // La clave foránea en Reserva será UsuarioId
                .OnDelete(DeleteBehavior.Cascade); // Configurar la acción de eliminación

            // Precarga de datos de Usuario
            modelBuilder.Entity<Usuario>().HasData(
         new Usuario { Id = 1, Nombre = "Juan Pérez" },
         new Usuario { Id = 2, Nombre = "María López" },
         new Usuario { Id = 3, Nombre = "Carlos Rodriguez" },
         new Usuario { Id = 4, Nombre = "Fernando Bonilla" },
         new Usuario { Id = 5, Nombre = "Nelson Gonzalez" },
         new Usuario { Id = 6, Nombre = "Ana García" },
         new Usuario { Id = 7, Nombre = "Luis Sánchez" },
         new Usuario { Id = 8, Nombre = "Elena Martínez" },
         new Usuario { Id = 9, Nombre = "David Fernández" },
         new Usuario { Id = 10, Nombre = "Sofia González" }
     );


            // Precarga de datos de Habitacion
            modelBuilder.Entity<Habitacion>().HasData(
          new Habitacion { HabitacionId = 1, Tipo = "Single", Precio = 100 },
          new Habitacion { HabitacionId = 2, Tipo = "Double", Precio = 150 },
          new Habitacion { HabitacionId = 3, Tipo = "Single", Precio = 90 },
          new Habitacion { HabitacionId = 4, Tipo = "Single", Precio = 90 },
          new Habitacion { HabitacionId = 5, Tipo = "Double", Precio = 160 },
          new Habitacion { HabitacionId = 6, Tipo = "Suite Deluxe", Precio = 400 },
          new Habitacion { HabitacionId = 7, Tipo = "Single", Precio = 85 },
          new Habitacion { HabitacionId = 8, Tipo = "Double", Precio = 155 },
          new Habitacion { HabitacionId = 9, Tipo = "Suite Premium", Precio = 500 }
      );

            // Precarga de datos de Reserva
            modelBuilder.Entity<Reserva>().HasData(
                new Reserva
                {
                    IDReserva = 1,
                    FechaInicio = new DateTime(2024, 12, 1),
                    FechaFin = new DateTime(2024, 12, 5),
                    EmailCliente = "juan.perez@gmail.com",
                    EstaPagada = true,
                    UsuarioId = 1, // Relación con el usuario Juan Pérez
                    HabitacionId = 1 // Relación con la habitación Single
                },
                new Reserva
                {
                    IDReserva = 2,
                    FechaInicio = new DateTime(2024, 12, 10),
                    FechaFin = new DateTime(2024, 12, 15),
                    EmailCliente = "maria.lopez@gmail.com",
                    EstaPagada = false,
                    UsuarioId = 2, // Relación con el usuario María López
                    HabitacionId = 2 // Relación con la habitación Double
                },
                new Reserva
                {
                    IDReserva = 3,
                    FechaInicio = new DateTime(2024, 12, 5),
                    FechaFin = new DateTime(2024, 12, 9),
                    EmailCliente = "carlos.rodriguez@gmail.com",
                    EstaPagada = true,
                    UsuarioId = 3, // Relación con el usuario Carlos Rodríguez
                    HabitacionId = 3 // Relación con la habitación Single
                },
                new Reserva
                {
                    IDReserva = 4,
                    FechaInicio = new DateTime(2024, 12, 12),
                    FechaFin = new DateTime(2024, 12, 16),
                    EmailCliente = "fernando.bonilla@gmail.com",
                    EstaPagada = true,
                    UsuarioId = 4, // Relación con el usuario Fernando Bonilla
                    HabitacionId = 4 // Relación con la habitación Single
                },
                new Reserva
                {
                    IDReserva = 5,
                    FechaInicio = new DateTime(2024, 12, 20),
                    FechaFin = new DateTime(2024, 12, 25),
                    EmailCliente = "nelson.gonzalez@gmail.com",
                    EstaPagada = false,
                    UsuarioId = 5, // Relación con el usuario Nelson Gonzalez
                    HabitacionId = 5 // Relación con la habitación Double
                },
                new Reserva
                {
                    IDReserva = 6,
                    FechaInicio = new DateTime(2024, 12, 18),
                    FechaFin = new DateTime(2024, 12, 22),
                    EmailCliente = "ana.garcia@gmail.com",
                    EstaPagada = true,
                    UsuarioId = 6, // Relación con el usuario Ana García
                    HabitacionId = 6 // Relación con la habitación Suite Deluxe
                },
                new Reserva
                {
                    IDReserva = 7,
                    FechaInicio = new DateTime(2024, 12, 8),
                    FechaFin = new DateTime(2024, 12, 12),
                    EmailCliente = "luis.sanchez@gmail.com",
                    EstaPagada = true,
                    UsuarioId = 7, // Relación con el usuario Luis Sánchez
                    HabitacionId = 7 // Relación con la habitación Single
                },
                new Reserva
                {
                    IDReserva = 8,
                    FechaInicio = new DateTime(2024, 12, 15),
                    FechaFin = new DateTime(2024, 12, 20),
                    EmailCliente = "elena.martinez@gmail.com",
                    EstaPagada = false,
                    UsuarioId = 8, // Relación con el usuario Elena Martínez
                    HabitacionId = 8 // Relación con la habitación Double
                },
                new Reserva
                {
                    IDReserva = 9,
                    FechaInicio = new DateTime(2024, 12, 1),
                    FechaFin = new DateTime(2024, 12, 3),
                    EmailCliente = "david.fernandez@gmail.com",
                    EstaPagada = true,
                    UsuarioId = 9, // Relación con el usuario David Fernández
                    HabitacionId = 9 // Relación con la habitación Suite Premium
                },
                new Reserva
                {
                    IDReserva = 10,
                    FechaInicio = new DateTime(2024, 12, 6),
                    FechaFin = new DateTime(2024, 12, 10),
                    EmailCliente = "sofia.gonzalez@gmail.com",
                    EstaPagada = false,
                    UsuarioId = 10, // Relación con el usuario Sofia González
                    HabitacionId = 5 // Relación con la habitación Double
                }
            );
        }
    }
}