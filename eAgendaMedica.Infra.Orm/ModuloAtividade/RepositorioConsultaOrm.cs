using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class RepositorioConsultaOrm : RepositorioBase<Consulta>, IRepositorioConsulta
    {
        public RepositorioConsultaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {

        }

        public override Consulta SelecionarPorId(Guid id)
        {
            return registros.Include(x => x.Medico).SingleOrDefault(x => x.Id == id);
        }

        public override async Task<Consulta> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Medico).SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<List<Consulta>> SelecionarTodosAsync()
        {
            return await registros.Include(x => x.Medico).ToListAsync();
        }

        public List<Consulta> SelecionarAtividadesFuturas(DateTime dataInicial, DateTime dataFinal)
        {
            return registros
              .Where(x => x.Data >= dataInicial)
              .Where(x => x.Data <= dataFinal)
              .ToList();
        }

        public List<Consulta> SelecionarAtividadesPassadas(DateTime dataDeHoje)
        {
            return registros
               .Where(x => x.Data < dataDeHoje)
               .ToList();
        }
    }
}
