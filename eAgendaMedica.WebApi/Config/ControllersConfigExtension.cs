using eAgendaMedica.WebApi.Filters;

namespace eAgendaMedica.WebApi.Config
{
    public static class ControllersConfigExtension
    {
        public static void ConfigurarControllers(this IServiceCollection services)
        {
            services.AddControllers(config => { config.Filters.Add<SerilogActionFilter>(); })
                    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()); });
        }
    }
}
