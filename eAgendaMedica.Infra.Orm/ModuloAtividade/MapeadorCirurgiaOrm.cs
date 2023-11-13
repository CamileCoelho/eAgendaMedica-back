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

            builder.Property(x => x.Id).ValueGeneratedNever(); builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.HoraInicio).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.HoraTermino).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.PeriodoRecuperacao).HasColumnType("bigint").IsRequired();


            //builder.HasMany(x => x.Medicos)
            //       .WithOne()
            //       .IsRequired()
            //       .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Medicos)
                   .WithOne()
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
