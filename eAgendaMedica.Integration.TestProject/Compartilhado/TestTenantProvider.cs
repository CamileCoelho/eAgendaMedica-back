using eAgendaMedica.Dominio.Compartilhado;

namespace eAgendaMedica.Integration.TestProject.Compartilhado
{
    public class TestTenantProvider : ITenantProvider
    {
        public Guid UsuarioId { get; set; }

        public TestTenantProvider(Guid guid)
        {
            UsuarioId = guid;
        }
    }
}
