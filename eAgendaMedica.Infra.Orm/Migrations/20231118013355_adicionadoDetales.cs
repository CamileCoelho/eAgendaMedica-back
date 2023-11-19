using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgendaMedica.Infra.Orm.Migrations
{
    public partial class adicionadoDetales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detalhes",
                table: "TBConsulta",
                type: "varchar(400)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detalhes",
                table: "TBCirurgia",
                type: "varchar(400)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detalhes",
                table: "TBConsulta");

            migrationBuilder.DropColumn(
                name: "Detalhes",
                table: "TBCirurgia");
        }
    }
}
