using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgendaMedica.Infra.Orm.Migrations
{
    public partial class migracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCirurgia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<long>(type: "bigint", nullable: false),
                    HoraTermino = table.Column<long>(type: "bigint", nullable: false),
                    PeriodoRecuperacao = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCirurgia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Crm = table.Column<string>(type: "varchar(20)", nullable: true),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBCirurgia_TBMedico",
                columns: table => new
                {
                    CirurgiasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCirurgia_TBMedico", x => new { x.CirurgiasId, x.MedicosId });
                    table.ForeignKey(
                        name: "FK_TBCirurgia_TBMedico_TBCirurgia_CirurgiasId",
                        column: x => x.CirurgiasId,
                        principalTable: "TBCirurgia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBCirurgia_TBMedico_TBMedico_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "TBMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBConsulta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<long>(type: "bigint", nullable: false),
                    HoraTermino = table.Column<long>(type: "bigint", nullable: false),
                    PeriodoRecuperacao = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBConsulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBConsulta_TBMedico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "TBMedico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBCirurgia_TBMedico_MedicosId",
                table: "TBCirurgia_TBMedico",
                column: "MedicosId");

            migrationBuilder.CreateIndex(
                name: "IX_TBConsulta_MedicoId",
                table: "TBConsulta",
                column: "MedicoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBCirurgia_TBMedico");

            migrationBuilder.DropTable(
                name: "TBConsulta");

            migrationBuilder.DropTable(
                name: "TBCirurgia");

            migrationBuilder.DropTable(
                name: "TBMedico");
        }
    }
}
