using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace eAgendaMedica.WebApi.Config
{
    public static class SwaggerConfigExtension
    {
        public static void ConfigurarSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c => {
                c.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("00:00")
                });
            });
        }
    }
}
