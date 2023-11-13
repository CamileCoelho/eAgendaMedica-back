using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.Compartilhado;

namespace eAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class RepositorioConsultaOrm : RepositorioBase<Atividade>, IRepositorioAtividade
    {
        public RepositorioConsultaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {

        }

        public List<Atividade> SelecionarAtividadesFuturas(DateTime dataInicial, DateTime dataFinal)
        {
            return registros
              .Where(x => x.Data >= dataInicial)
              .Where(x => x.Data <= dataFinal)
              .ToList();
        }

        public List<Atividade> SelecionarAtividadesPassadas(DateTime dataDeHoje)
        {
            return registros
               .Where(x => x.Data < dataDeHoje)
               .ToList();
        }
    }
}
