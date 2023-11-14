using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Infra.Orm.Compartilhado;

namespace eAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class RepositorioCirurgiaOrm : RepositorioBase<Cirurgia>, IRepositorioCirurgia
    {
        public RepositorioCirurgiaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {

        }

        public List<Cirurgia> SelecionarAtividadesFuturas(DateTime dataInicial, DateTime dataFinal)
        {
            return registros
              .Where(x => x.Data >= dataInicial)
              .Where(x => x.Data <= dataFinal)
              .ToList();
        }

        public List<Cirurgia> SelecionarAtividadesPassadas(DateTime dataDeHoje)
        {
            return registros
               .Where(x => x.Data < dataDeHoje)
               .ToList();
        }
    }
}
