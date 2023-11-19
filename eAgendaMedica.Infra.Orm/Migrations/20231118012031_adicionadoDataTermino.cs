using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgendaMedica.Infra.Orm.Migrations
{
    public partial class adicionadoDataTermino : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "TBConsulta",
                newName: "DataTermino");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "TBCirurgia",
                newName: "DataTermino");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "TBConsulta",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "TBCirurgia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "TBConsulta");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "TBCirurgia");

            migrationBuilder.RenameColumn(
                name: "DataTermino",
                table: "TBConsulta",
                newName: "Data");

            migrationBuilder.RenameColumn(
                name: "DataTermino",
                table: "TBCirurgia",
                newName: "Data");
        }
    }
}
