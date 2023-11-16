using eAgendaMedica.Aplicacao.ModuloAtividade;
using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm;
using eAgendaMedica.Infra.Orm.ModuloAtividade;
using eAgendaMedica.Infra.Orm.ModuloMedico;

namespace eAgendaMedica.WebApi.Config
{
    public static class InjecaoDependenciaConfigExtension
    {
        public static void ConfigurarInjecaoDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContext<IContextoPersistencia, eAgendaMedicaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });

            services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
            services.AddTransient<ServicoMedico>();

            services.AddScoped<IRepositorioConsulta, RepositorioConsultaOrm>();
            services.AddTransient<ServicoConsulta>();

            services.AddScoped<IRepositorioCirurgia, RepositorioCirurgiaOrm>();
            services.AddTransient<ServicoCirurgia>();

            services.AddTransient<ConfigFormsCirurgiaMappingAction>();
        }
    }
}
