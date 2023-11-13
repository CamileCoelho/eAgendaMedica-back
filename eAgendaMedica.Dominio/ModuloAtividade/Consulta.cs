using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Consulta : Atividade
    {
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

        public void AtualizarInformacoes(Consulta registroAtualizado)
        {
            Data = registroAtualizado.Data;
            HoraInicio = registroAtualizado.HoraInicio;
            HoraTermino = registroAtualizado.HoraTermino;
            PeriodoRecuperacao = registroAtualizado.PeriodoRecuperacao;
            Medico = registroAtualizado.Medico;
        }
    }
}
