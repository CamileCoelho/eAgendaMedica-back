using eAgendaMedica.Dominio.Compartilhado;
using System.Security.Claims;

namespace eAgendaMedica.WebApi.Config
{
    public class ApiTenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApiTenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid UsuarioId
        {
            get
            {
                var id = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(id))
                    return Guid.Empty;

                return Guid.Parse(id);
            }
        }
    }
}
