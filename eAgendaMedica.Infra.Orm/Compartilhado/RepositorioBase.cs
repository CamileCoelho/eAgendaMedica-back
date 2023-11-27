using eAgendaMedica.Dominio.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace eAgendaMedica.Infra.Orm.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase<T>
    {
        protected readonly eAgendaMedicaDbContext dbContext;
        protected DbSet<T> registros;

        public RepositorioBase(IContextoPersistencia contextoPersistencia)
        {
            dbContext = (eAgendaMedicaDbContext)contextoPersistencia;

            registros = dbContext.Set<T>();
        }

        public virtual async Task<bool> InserirAsync(T novoRegistro)
        {
            await registros.AddAsync(novoRegistro);

            return true;
        }

        public virtual void Editar(T registro)
        {
            registros.Update(registro);
        }

        public virtual void Excluir(T registro)
        {
            registros.Remove(registro);
        }

        public virtual T SelecionarPorId(Guid id)
        {
            return registros.SingleOrDefault(x => x.Id == id);
        }

        public virtual async Task<T> SelecionarPorIdAsync(Guid id)
        {
            return await registros.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<T>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }

        public virtual void InserirTest(T novoRegistro)
        {
            registros.Add(novoRegistro);
        }
    }
}
