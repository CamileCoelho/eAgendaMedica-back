using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.Compartilhado;

namespace eAgendaMedica.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>, IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {
        }

        public List<Medico> SelecionarMuitos(List<Guid> medicoIds)
        {
            return registros.Where(medico => medicoIds.Contains(medico.Id)).ToList();
        }
    }
}
