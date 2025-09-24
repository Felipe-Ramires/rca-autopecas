using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RcaAutopecas.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class RefactorApplicationUserAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Rua",
                table: "AspNetUsers",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "AspNetUsers",
                newName: "RazaoSocial");

            migrationBuilder.RenameColumn(
                name: "NomeDaEmpresa",
                table: "AspNetUsers",
                newName: "RamoDeAtividade");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "AspNetUsers",
                newName: "NomeFantasia");

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Localidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ibge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ddd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Siafi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ApplicationUserId",
                table: "Enderecos",
                column: "ApplicationUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "AspNetUsers",
                newName: "Rua");

            migrationBuilder.RenameColumn(
                name: "RazaoSocial",
                table: "AspNetUsers",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "RamoDeAtividade",
                table: "AspNetUsers",
                newName: "NomeDaEmpresa");

            migrationBuilder.RenameColumn(
                name: "NomeFantasia",
                table: "AspNetUsers",
                newName: "Estado");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
