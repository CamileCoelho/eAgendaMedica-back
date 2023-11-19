using eAgendaMedica.Aplicacao.ModuloAutenticacao;
using eAgendaMedica.Infra.Orm;
using Microsoft.AspNetCore.Identity;

namespace eAgendaMedica.WebApi.Config
{
    public static class IdentityConfigExtension
    {
        public static void ConfigurarIdentity(this IServiceCollection services)
        {
            services.AddTransient<ServicoAutenticacao>();
            services.AddTransient<AspNetUserManager<Usuario>>();

            services.AddIdentity<Usuario, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
           .AddEntityFrameworkStores<eAgendaMedicaDbContext>()
           .AddDefaultTokenProviders()
           .AddErrorDescriber<eAgendaMedicaErrorDescriber>();
        }
    }
}
