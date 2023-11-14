using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Consulta : EntidadeBase<Consulta>
    {
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public Medico Medico { get; set; }

        public Consulta()
        {
            Data = DateTime.Now;
            HoraInicio = Data.TimeOfDay;
            HoraTermino = Data.TimeOfDay;
        }

        public Consulta(DateTime data, TimeSpan horaInicio, TimeSpan horaTermino, Medico medico)
        {
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            PeriodoRecuperacao = TimeSpan.FromMinutes(20);
            Medico = medico;        
        }

        public override void AtualizarInformacoes(Consulta registroAtualizado)
        {
            Data = registroAtualizado.Data;
            HoraInicio = registroAtualizado.HoraInicio;
            HoraTermino = registroAtualizado.HoraTermino;
            PeriodoRecuperacao = registroAtualizado.PeriodoRecuperacao;
            Medico = registroAtualizado.Medico;
        }
    }
}
