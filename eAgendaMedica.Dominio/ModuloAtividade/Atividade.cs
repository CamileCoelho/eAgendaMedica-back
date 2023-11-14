namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public abstract class Atividade : EntidadeBase<Atividade>
    {
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }

        public Atividade()
        {
           
        }
           
        public override void AtualizarInformacoes(Atividade registroAtualizado)
        {

        }
    }
}
