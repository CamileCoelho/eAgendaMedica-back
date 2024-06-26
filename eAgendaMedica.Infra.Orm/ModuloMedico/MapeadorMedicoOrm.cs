﻿using eAgendaMedica.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgendaMedica.Infra.Orm.ModuloMedico
{
    public class MapeadorMedicoOrm : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("TBMedico");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Crm).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Especialidade).HasColumnType("varchar(300)").IsRequired();

            builder.HasMany(x => x.Cirurgias)
                   .WithMany(x => x.Medicos);

            builder.HasMany(x => x.Consultas)
                   .WithOne(x => x.Medico);

            builder.HasOne(x => x.Usuario)
                   .WithMany()
                   .IsRequired()
                   .HasForeignKey(x => x.UsuarioId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
