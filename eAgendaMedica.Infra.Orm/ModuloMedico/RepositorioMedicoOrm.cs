using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.ModuloMedico
{
    public class RepositorioMedicoOrm : RepositorioBase<Medico>, IRepositorioMedico
    {
        public RepositorioMedicoOrm(IContextoPersistencia contextoPersistencia) : base(contextoPersistencia)
        {

        }

        public override async Task<Medico> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Consultas).Include(y => y.Cirurgias).SingleOrDefaultAsync(x => x.Id == id);
        }
        
        public override Medico SelecionarPorId(Guid id)
        {
            return registros.Include(x => x.Consultas).Include(y => y.Cirurgias).SingleOrDefault(x => x.Id == id);
        }

        public List<Medico> SelecionarMuitos(List<Guid> medicoIds)
        {
            return registros.Include(x => x.Consultas).Include(y => y.Cirurgias).Where(medico => medicoIds.Contains(medico.Id)).ToList();
        }

        public Medico SelecionarPorCrm(string crm)
        {
            return registros.FirstOrDefault(x => x.Crm == crm);
        }

        public bool MedicoNaoPodeSerExcluido(Medico medico)
        {
            return registros.Any(x => 
                            x.Consultas.Any(y => y.MedicoId == medico.Id) || 
                            x.Cirurgias.Any(y => y.Medicos.Any(z => z.Id == medico.Id)));
        }
    }
}
