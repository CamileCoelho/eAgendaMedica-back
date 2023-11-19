namespace eAgendaMedica.WebApi.Config
{
    public static class AutoMapperConfigExtension
    {
        public static void ConfigurarAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<MedicoProfile>();
                config.AddProfile<CirurgiaProfile>();
                config.AddProfile<ConsultaProfile>();
                config.AddProfile<UsuarioProfile>();
            });
        }
    }
}
