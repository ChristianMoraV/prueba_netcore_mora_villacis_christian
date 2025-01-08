using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infraestructure.Persistences.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pelicula",
                columns: table => new
                {
                    id_pelicula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    duracion = table.Column<int>(type: "int", nullable: false),
                    auditCreateUser = table.Column<int>(type: "int", nullable: false),
                    auditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    auditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    auditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    auditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    auditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    state = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pelicula", x => x.id_pelicula);
                });

            migrationBuilder.CreateTable(
                name: "sala_cine",
                columns: table => new
                {
                    id_sala = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    auditCreateUser = table.Column<int>(type: "int", nullable: false),
                    auditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    auditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    auditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    auditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    auditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    state = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sala_cine", x => x.id_sala);
                });

            migrationBuilder.CreateTable(
                name: "pelicula_sala",
                columns: table => new
                {
                    id_pelicula_sala = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_sala_cine = table.Column<int>(type: "int", nullable: false),
                    id_pelicula = table.Column<int>(type: "int", nullable: false),
                    fecha_publicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    auditCreateUser = table.Column<int>(type: "int", nullable: false),
                    auditCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    auditUpdateUser = table.Column<int>(type: "int", nullable: true),
                    auditUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    auditDeleteUser = table.Column<int>(type: "int", nullable: true),
                    auditDeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    state = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pelicula_sala", x => x.id_pelicula_sala);
                    table.ForeignKey(
                        name: "FK__pelicula___id_pe__4222D4EF",
                        column: x => x.id_pelicula,
                        principalTable: "pelicula",
                        principalColumn: "id_pelicula");
                    table.ForeignKey(
                        name: "FK__pelicula___id_sa__412EB0B6",
                        column: x => x.id_sala_cine,
                        principalTable: "sala_cine",
                        principalColumn: "id_sala");
                });

            migrationBuilder.InsertData(
                table: "sala_cine",
                columns: new[] { "id_sala", "auditCreateDate", "auditCreateUser", "auditDeleteDate", "auditDeleteUser", "auditUpdateDate", "auditUpdateUser", "estado", "nombre", "state" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 6, 19, 14, 22, 620, DateTimeKind.Local).AddTicks(3926), 1, null, null, null, null, "Disponible", "Sala 1", 1 },
                    { 2, new DateTime(2025, 1, 6, 19, 14, 22, 620, DateTimeKind.Local).AddTicks(3937), 1, null, null, null, null, "Disponible", "Sala 2", 1 },
                    { 3, new DateTime(2025, 1, 6, 19, 14, 22, 620, DateTimeKind.Local).AddTicks(3938), 1, null, null, null, null, "Disponible", "Sala 3", 1 },
                    { 4, new DateTime(2025, 1, 6, 19, 14, 22, 620, DateTimeKind.Local).AddTicks(3940), 1, null, null, null, null, "Disponible", "Sala 4", 1 },
                    { 5, new DateTime(2025, 1, 6, 19, 14, 22, 620, DateTimeKind.Local).AddTicks(3940), 1, null, null, null, null, "Disponible", "Sala 5", 1 },
                    { 6, new DateTime(2025, 1, 6, 19, 14, 22, 620, DateTimeKind.Local).AddTicks(3941), 1, null, null, null, null, "Disponible", "Sala 6", 1 },
                    { 7, new DateTime(2025, 1, 6, 19, 14, 22, 620, DateTimeKind.Local).AddTicks(3942), 1, null, null, null, null, "Disponible", "Sala 7", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_pelicula_sala_id_pelicula",
                table: "pelicula_sala",
                column: "id_pelicula");

            migrationBuilder.CreateIndex(
                name: "IX_pelicula_sala_id_sala_cine",
                table: "pelicula_sala",
                column: "id_sala_cine");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pelicula_sala");

            migrationBuilder.DropTable(
                name: "pelicula");

            migrationBuilder.DropTable(
                name: "sala_cine");
        }
    }
}
