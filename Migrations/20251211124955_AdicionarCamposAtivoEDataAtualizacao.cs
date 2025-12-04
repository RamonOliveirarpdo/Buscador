using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buscador.Migrations
{
    public partial class AdicionarCamposAtivoEDataAtualizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Situacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Situacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Situacoes");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Situacoes");
        }
    }
}
