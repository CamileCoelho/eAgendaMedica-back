namespace eAgendaMedica.Dominio.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        Task<bool> InserirAsync(T registro);

        void Editar(T registro);

        void Excluir(T registro);

        T SelecionarPorId(Guid id);

        Task<T> SelecionarPorIdAsync(Guid id);

        Task<List<T>> SelecionarTodosAsync();
    }
}
