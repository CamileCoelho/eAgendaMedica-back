using Serilog;
using System.Text.Json;

namespace eAgendaMedica.WebApi.Config
{
    public class ManipuladorDeExcecoes
    {
        private readonly RequestDelegate requestDelegate;

        public ManipuladorDeExcecoes(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await this.requestDelegate(ctx);
            }
            catch (Exception ex)
            {
                ctx.Response.StatusCode = 500;
                ctx.Response.ContentType = "application/json";

                var problema = new
                {
                    Sucesso = false,
                    Erros = new List<string> { ex.Message }
                };

                Log.Logger.Error(ex, ex.Message);

                ctx.Response.WriteAsync(JsonSerializer.Serialize(problema));
            }
        }
    }
}
