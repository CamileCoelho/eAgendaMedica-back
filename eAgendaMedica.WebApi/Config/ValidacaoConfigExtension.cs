namespace eAgendaMedica.WebApi.Config
{
    public static class ValidacaoConfigExtension
    {
        public static void ConfigurarValidacao(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
