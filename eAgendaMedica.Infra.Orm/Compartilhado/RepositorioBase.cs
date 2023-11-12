using eAgendaMedica.Dominio.Compartilhado;

namespace eAgendaMedica.Infra.Orm.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase<T>
    {
        protected DbSet<T> registros;
        private readonly eAgendaMedicaDbContext dbContext;

        public RepositorioBase(IContextoPersistencia contextoPersistencia)
        {
            dbContext = (eAgendaMedicaDbContext)contextoPersistencia;

            registros = dbContext.Set<T>();
        }
        
        public virtual void Inserir(T novoRegistro)
        {
            registros.Add(novoRegistro);
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

        public virtual List<T> SelecionarTodos()
        {
            return registros.ToList();
        }

        public virtual async Task<bool> InserirAsync(T novoRegistro)
        {
            await registros.AddAsync(novoRegistro);

            return true;
        }

        public virtual async Task<T> SelecionarPorIdAsync(Guid id)
        {
            return await registros.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<T>> SelecionarTodosAsync()
        {
            return await registros.ToListAsync();
        }
    }
}
