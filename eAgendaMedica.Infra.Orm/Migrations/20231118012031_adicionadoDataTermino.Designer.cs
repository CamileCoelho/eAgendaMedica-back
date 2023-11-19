﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eAgendaMedica.Infra.Orm;

#nullable disable

namespace eAgendaMedica.Infra.Orm.Migrations
{
    [DbContext(typeof(eAgendaMedicaDbContext))]
    [Migration("20231118012031_adicionadoDataTermino")]
    partial class adicionadoDataTermino
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CirurgiaMedico", b =>
                {
                    b.Property<Guid>("CirurgiasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MedicosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CirurgiasId", "MedicosId");

                    b.HasIndex("MedicosId");

                    b.ToTable("TBCirurgia_TBMedico", (string)null);
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloAtividade.Cirurgia", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataTermino")
                        .HasColumnType("datetime2");

                    b.Property<long>("HoraInicio")
                        .HasColumnType("bigint");

                    b.Property<long>("HoraTermino")
                        .HasColumnType("bigint");

                    b.Property<long>("PeriodoRecuperacao")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("TBCirurgia", (string)null);
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloAtividade.Consulta", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataTermino")
                        .HasColumnType("datetime2");

                    b.Property<long>("HoraInicio")
                        .HasColumnType("bigint");

                    b.Property<long>("HoraTermino")
                        .HasColumnType("bigint");

                    b.Property<Guid>("MedicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("PeriodoRecuperacao")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.ToTable("TBConsulta", (string)null);
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloMedico.Medico", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Crm")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Especialidade")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TBMedico", (string)null);
                });

            modelBuilder.Entity("CirurgiaMedico", b =>
                {
                    b.HasOne("eAgendaMedica.Dominio.ModuloAtividade.Cirurgia", null)
                        .WithMany()
                        .HasForeignKey("CirurgiasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAgendaMedica.Dominio.ModuloMedico.Medico", null)
                        .WithMany()
                        .HasForeignKey("MedicosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eAgendaMedica.Dominio.ModuloAtividade.Consulta", b =>
                {
                    b.HasOne("eAgendaMedica.Dominio.ModuloMedico.Medico", "Medico")
                        .WithMany()
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Medico");
                });
#pragma warning restore 612, 618
        }
    }
}
