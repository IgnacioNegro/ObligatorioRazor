using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObligatorioRazor.Migrations
{
    public partial class ok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habitacion",
                columns: table => new
                {
                    HabitacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacion", x => x.HabitacionId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ReservaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstaPagada = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    HabitacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ReservaID);
                    table.ForeignKey(
                        name: "FK_Reserva_Habitacion_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitacion",
                        principalColumn: "HabitacionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Habitacion",
                columns: new[] { "HabitacionId", "Precio", "Tipo" },
                values: new object[,]
                {
                    { 1, 100, "Single" },
                    { 2, 150, "Double" },
                    { 3, 90, "Single" },
                    { 4, 90, "Single" },
                    { 5, 160, "Double" },
                    { 6, 400, "Suite Deluxe" },
                    { 7, 85, "Single" },
                    { 8, 155, "Double" },
                    { 9, 500, "Suite Premium" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioID", "Nombre" },
                values: new object[,]
                {
                    { 1, "Juan Pérez" },
                    { 2, "María López" },
                    { 3, "Carlos Rodriguez" },
                    { 4, "Fernando Bonilla" },
                    { 5, "Nelson Gonzalez" },
                    { 6, "Ana García" },
                    { 7, "Luis Sánchez" },
                    { 8, "Elena Martínez" },
                    { 9, "David Fernández" },
                    { 10, "Sofia González" }
                });

            migrationBuilder.InsertData(
                table: "Reserva",
                columns: new[] { "ReservaID", "EmailCliente", "EstaPagada", "FechaFin", "FechaInicio", "FechaReserva", "HabitacionId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, "juan.perez@gmail.com", true, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 2, "maria.lopez@gmail.com", false, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 3, "carlos.rodriguez@gmail.com", true, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 4, "fernando.bonilla@gmail.com", true, new DateTime(2024, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 4, 4 },
                    { 5, "nelson.gonzalez@gmail.com", false, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 5, 5 },
                    { 6, "ana.garcia@gmail.com", true, new DateTime(2024, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 6, 6 },
                    { 7, "luis.sanchez@gmail.com", true, new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 7, 7 },
                    { 8, "elena.martinez@gmail.com", false, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 8, 8 },
                    { 9, "david.fernandez@gmail.com", true, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 9, 9 },
                    { 10, "sofia.gonzalez@gmail.com", false, new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Local), 5, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_HabitacionId",
                table: "Reserva",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_UsuarioId",
                table: "Reserva",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Habitacion");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
