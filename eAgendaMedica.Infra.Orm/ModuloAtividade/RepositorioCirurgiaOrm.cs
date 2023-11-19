using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace eAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class RepositorioCirurgiaOrm : RepositorioBase<Cirurgia>, IRepositorioCirurgia
    {
        public RepositorioCirurgiaOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {

        }

        public override Cirurgia SelecionarPorId(Guid id)
        {
            return registros.Include(x => x.Medicos).SingleOrDefault(x => x.Id == id);
        }

        public override async Task<Cirurgia> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Medicos).SingleOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<List<Cirurgia>> SelecionarTodosAsync()
        {
            return await registros.Include(x => x.Medicos).ToListAsync();
        }

        public List<Cirurgia> SelecionarAtividadesFuturas(DateTime dataInicial, DateTime dataFinal)
        {
            return registros
              .Where(x => x.DataInicio >= dataInicial)
              .Where(x => x.DataInicio <= dataFinal)
              .ToList();
        }

        public List<Cirurgia> SelecionarAtividadesPassadas(DateTime dataDeHoje)
        {
            return registros
               .Where(x => x.DataInicio < dataDeHoje)
               .ToList();
        }
    }
}
