using Taikandi;

namespace eAgendaMedica.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }
        public EntidadeBase() { Id = SequentialGuid.NewGuid(); }
        public abstract void AtualizarInformacoes(T registroAtualizado);
    }
}
