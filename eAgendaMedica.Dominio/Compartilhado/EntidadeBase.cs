using eAgendaMedica.Aplicacao.ModuloAutenticacao;
using Taikandi;

namespace eAgendaMedica.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioId { get; set; }
        public EntidadeBase() { Id = SequentialGuid.NewGuid(); }
        public abstract void AtualizarInformacoes(T registroAtualizado);
    }
}
