using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaducaRest.Migrations
{
    public partial class IndicesCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categoria",
                type: "VARCHAR(80)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "UI_CategoriaClave",
                table: "Categoria",
                column: "Clave",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UI_CategoriaClave",
                table: "Categoria");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categoria",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)");
        }
    }
}
