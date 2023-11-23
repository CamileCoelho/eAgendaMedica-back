namespace eAgendaMedica.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigurarValidacao();
            builder.Services.ConfigurarIdentity();
            builder.Services.ConfigurarSerilog(builder.Logging);
            builder.Services.ConfigurarAutoMapper();
            builder.Services.ConfigurarInjecaoDependencia(builder.Configuration);
            builder.Services.ConfigurarSwagger();
            builder.Services.ConfigurarControllers();
            builder.Services.ConfigurarJwt();
            builder.Services.ConfigurarCors("Desenvolvimento");

            var app = builder.Build();

            app.MigrateDatabase();
            app.UseMiddleware<ManipuladorDeExcecoes>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("Desenvolvimento");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}