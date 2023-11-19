using eAgendaMedica.Aplicacao.ModuloAutenticacao;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Infra.Orm.ModuloAtividade;
using eAgendaMedica.Infra.Orm.ModuloMedico;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm
{
    public class eAgendaMedicaDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>, IContextoPersistencia
    {
        public eAgendaMedicaDbContext(DbContextOptions options) : base(options)
        {

        }

        public async Task<bool> GravarDadosAsync()
        {
            await SaveChangesAsync();
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapeadorMedicoOrm());
            
            modelBuilder.ApplyConfiguration(new MapeadorConsultaOrm());

            modelBuilder.ApplyConfiguration(new MapeadorCirurgiaOrm());

            base.OnModelCreating(modelBuilder);
        }
    }
}
