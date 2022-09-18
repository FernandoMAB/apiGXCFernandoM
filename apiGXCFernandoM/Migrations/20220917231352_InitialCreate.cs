using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiGXCFernandoM.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colegio",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    tipoColegio = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colegio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreCompleto = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    username = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    password = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    correoElectronico = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    rol = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idColegio = table.Column<int>(type: "int", nullable: true),
                    nombre = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    area = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true),
                    numeroEstudiantes = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    docenteAsignado = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    curso = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    paralelo = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materia_Colegio_idColegio",
                        column: x => x.idColegio,
                        principalTable: "Colegio",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Calificacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idColegio = table.Column<int>(type: "int", nullable: false),
                    idMateria = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    calificacion = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificacion_Colegio_idColegio",
                        column: x => x.idColegio,
                        principalTable: "Colegio",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Calificacion_Materia_idMateria",
                        column: x => x.idMateria,
                        principalTable: "Materia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Calificacion_Usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Colegio",
                columns: new[] { "id", "nombre", "tipoColegio" },
                values: new object[] { 1, "Nombre de Prueba", "Privado" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "correoElectronico", "nombreCompleto", "password", "rol", "username" },
                values: new object[] { 1, "fer@fer.com", "Usuario Prueba", "admin", "admin", "Fer" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "correoElectronico", "nombreCompleto", "password", "rol", "username" },
                values: new object[] { 2, "john@fer.com", "John Doe", "john", "seller", "John" });

            migrationBuilder.InsertData(
                table: "Materia",
                columns: new[] { "Id", "area", "curso", "docenteAsignado", "idColegio", "nombre", "numeroEstudiantes", "paralelo" },
                values: new object[] { 1, "Area Prueba", "Primero", "Docente Prueba", 1, "Nombre Prueba", 10, "A" });

            migrationBuilder.InsertData(
                table: "Calificacion",
                columns: new[] { "Id", "calificacion", "idColegio", "idMateria", "idUsuario" },
                values: new object[] { 1, 5m, 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_idColegio",
                table: "Calificacion",
                column: "idColegio");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_idMateria",
                table: "Calificacion",
                column: "idMateria");

            migrationBuilder.CreateIndex(
                name: "IX_Calificacion_idUsuario",
                table: "Calificacion",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Materia_idColegio",
                table: "Materia",
                column: "idColegio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificacion");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Colegio");
        }
    }
}
