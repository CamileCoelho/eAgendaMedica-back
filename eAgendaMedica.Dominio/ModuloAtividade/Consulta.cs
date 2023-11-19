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
            DataTermino = DataInicio;
            HoraInicio = DataInicio.TimeOfDay;
            HoraTermino = DataInicio.TimeOfDay;
            PeriodoRecuperacao = TimeSpan.FromMinutes(20);
            Medico = new Medico();
        }

        public Consulta(string? detalhes, DateTime data, TimeSpan horaInicio, TimeSpan horaTermino, Medico medico)
        {
            Detalhes = detalhes;
            DataInicio = data; 
            DataTermino = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            PeriodoRecuperacao = TimeSpan.FromMinutes(20);

            TimeSpan novaHora = HoraTermino.Add(PeriodoRecuperacao);

            while (novaHora.TotalHours >= 24)
            {
                DataTermino = DataTermino.AddDays(1);
                novaHora = novaHora.Subtract(TimeSpan.FromHours(24));
            }

            HoraTermino = novaHora;
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
