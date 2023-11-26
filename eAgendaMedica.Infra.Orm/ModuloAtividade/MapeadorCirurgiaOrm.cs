using eAgendaMedica.Dominio.ModuloAtividade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class MapeadorCirurgiaOrm : IEntityTypeConfiguration<Cirurgia>
    {
        public void Configure(EntityTypeBuilder<Cirurgia> builder)
        {
            builder.ToTable("TBCirurgia");

            builder.Property(x => x.Id).ValueGeneratedNever(); 
            builder.Property(x => x.DataInicio).IsRequired();
            builder.Property(x => x.Detalhes).HasColumnType("varchar(400)").IsRequired(false);
            builder.Property(x => x.DataInicio).IsRequired();
            builder.Property(x => x.DataTermino).IsRequired();
            builder.Property(x => x.HoraInicio).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.HoraTermino).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.PeriodoRecuperacao).HasColumnType("bigint").IsRequired();

            builder.HasMany(x => x.Medicos)
                   .WithMany(x => x.Cirurgias)
                   .UsingEntity(x => x.ToTable("TBCirurgia_TBMedico"));

            builder.HasOne(x => x.Usuario)
                   .WithMany()
                   .IsRequired()
                   .HasForeignKey(x => x.UsuarioId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
