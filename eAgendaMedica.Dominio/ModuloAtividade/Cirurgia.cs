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
            DataTermino = DataInicio;
            HoraInicio = DataInicio.TimeOfDay;
            HoraTermino = DataInicio.TimeOfDay;
            PeriodoRecuperacao = TimeSpan.FromMinutes(240);
            Medicos = new List<Medico>();
        }

        public Cirurgia(string? detalhes, DateTime data, TimeSpan horaInicio, TimeSpan horaTermino, List<Medico> medicos)
        {
            Detalhes = detalhes;
            DataInicio = data;
            DataTermino = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            PeriodoRecuperacao = TimeSpan.FromMinutes(240);

            TimeSpan novaHora = HoraTermino.Add(PeriodoRecuperacao);

            while (novaHora.TotalHours >= 24)
            {
                DataTermino = DataTermino.AddDays(1);
                novaHora = novaHora.Subtract(TimeSpan.FromHours(24));
            }

            HoraTermino = novaHora;
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
