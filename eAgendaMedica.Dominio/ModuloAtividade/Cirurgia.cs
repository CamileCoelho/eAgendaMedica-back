using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Cirurgia : Atividade
    {
        public List<Medico> Medicos { get; set; }

        public Cirurgia()
        {
            Data = DateTime.Now;
            HoraInicio = Data.TimeOfDay;
            HoraTermino = Data.TimeOfDay;
        }

        public Cirurgia(DateTime data, TimeSpan horaInicio, TimeSpan horaTermino, List<Medico> medicos)
        {
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            PeriodoRecuperacao = TimeSpan.FromMinutes(240); // 4 horas em minutos
            Medicos = medicos;
        }

        public void AtualizarInformacoes(Cirurgia registroAtualizado)
        {
            Data = registroAtualizado.Data;
            HoraInicio = registroAtualizado.HoraInicio;
            HoraTermino = registroAtualizado.HoraTermino;
            PeriodoRecuperacao = registroAtualizado.PeriodoRecuperacao;
            Medicos = registroAtualizado.Medicos;
        }
    }
}
