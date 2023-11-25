using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Cirurgia : EntidadeBase<Cirurgia>
    {
        public string? Detalhes { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public List<Medico> Medicos { get; set; }

        public Cirurgia()
        {
            DataInicio = DateTime.Now;
            DataTermino = DateTime.Now;
            HoraInicio = DataInicio.TimeOfDay;
            HoraTermino = DataInicio.TimeOfDay;
            PeriodoRecuperacao = TimeSpan.FromMinutes(240);
            Medicos = new List<Medico>();
        }

        public Cirurgia(string? detalhes, DateTime dataInicio, DateTime dataTermino, TimeSpan horaInicio, TimeSpan horaTermino, List<Medico> medicos)
        {
            Detalhes = detalhes;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;

            DataInicio = DataInicio.Date + HoraInicio;
            DataTermino = DataTermino.Date + HoraTermino;

            PeriodoRecuperacao = TimeSpan.FromMinutes(240);
            Medicos = medicos;
        }

        public override void AtualizarInformacoes(Cirurgia registroAtualizado)
        {
            Detalhes = registroAtualizado.Detalhes;
            DataInicio = registroAtualizado.DataInicio;
            DataTermino = registroAtualizado.DataTermino;
            HoraInicio = registroAtualizado.HoraInicio;
            HoraTermino = registroAtualizado.HoraTermino;
            PeriodoRecuperacao = registroAtualizado.PeriodoRecuperacao;
            Medicos = registroAtualizado.Medicos;
        }
    }
}
