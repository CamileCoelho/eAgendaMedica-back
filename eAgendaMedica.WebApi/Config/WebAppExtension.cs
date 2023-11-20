using eAgendaMedica.Infra.Orm;

namespace eAgendaMedica.WebApi.Config
{
    public static class WebAppExtension
    {
        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<eAgendaMedicaDbContext>();

                dataContext.Database.Migrate();
            }
        }
    }
}