using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Consulta : EntidadeBase<Consulta>
    {
        public string? Detalhes { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public Medico Medico { get; set; }
        public Guid MedicoId { get; set; }

        public Consulta()
        {
            DataInicio = DateTime.Now;
            DataTermino = DateTime.Now; 
            HoraInicio = DataInicio.TimeOfDay;
            HoraTermino = DataInicio.TimeOfDay;
            PeriodoRecuperacao = TimeSpan.FromMinutes(20);
            Medico = new Medico();
        }

        public Consulta(string? detalhes, DateTime dataInicio, DateTime dataTermino, TimeSpan horaInicio, TimeSpan horaTermino, Medico medico)
        {
            Detalhes = detalhes;
            DataInicio = dataInicio; 
            DataTermino = dataTermino;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;

            DataInicio = DataInicio.Date + HoraInicio;
            DataTermino = DataTermino.Date + HoraTermino;

            PeriodoRecuperacao = TimeSpan.FromMinutes(20);
            HoraTermino = horaTermino;
            Medico = medico;
        }

        public override void AtualizarInformacoes(Consulta registroAtualizado)
        {
            Detalhes = registroAtualizado.Detalhes;
            DataInicio = registroAtualizado.DataInicio;
            DataTermino = registroAtualizado.DataTermino;
            HoraInicio = registroAtualizado.HoraInicio;
            HoraTermino = registroAtualizado.HoraTermino;
            PeriodoRecuperacao = registroAtualizado.PeriodoRecuperacao;
            Medico = registroAtualizado.Medico;
        }
    }
}
