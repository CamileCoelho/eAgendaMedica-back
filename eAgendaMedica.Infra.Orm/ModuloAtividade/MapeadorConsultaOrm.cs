using eAgendaMedica.Dominio.ModuloAtividade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class MapeadorConsultaOrm : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("TBConsulta");

            builder.Property(x => x.Id).ValueGeneratedNever();builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.HoraInicio).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.HoraTermino).HasColumnType("bigint").IsRequired();
            builder.Property(x => x.PeriodoRecuperacao).HasColumnType("bigint").IsRequired();

            //builder.HasOne(x => x.Medico)
            //       .WithOne()
            //       .IsRequired()
            //       .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Medico)
                   .WithMany() 
                   .IsRequired() 
                   .HasForeignKey("MedicoId") 
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
