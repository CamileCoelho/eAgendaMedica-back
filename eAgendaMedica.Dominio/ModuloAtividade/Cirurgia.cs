using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Cirurgia : EntidadeBase<Cirurgia>
    {
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public List<Medico> Medicos { get; set; }

        public Cirurgia()
        {
            Data = DateTime.Now;
            HoraInicio = Data.TimeOfDay;
            HoraTermino = Data.TimeOfDay;
            PeriodoRecuperacao = TimeSpan.FromMinutes(240);
            Medicos = new List<Medico>();
        }

        public Cirurgia(DateTime data, TimeSpan horaInicio, TimeSpan horaTermino, List<Medico> medicos)
        {
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            PeriodoRecuperacao = TimeSpan.FromMinutes(240);

            TimeSpan novaHora = HoraTermino.Add(PeriodoRecuperacao);

            while (novaHora.TotalHours >= 24)
            {
                Data = Data.AddDays(1);
                novaHora = novaHora.Subtract(TimeSpan.FromHours(24));
            }

            HoraTermino = novaHora;
            Medicos = medicos;
        }

        public override void AtualizarInformacoes(Cirurgia registroAtualizado)
        {
            Data = registroAtualizado.Data;
            HoraInicio = registroAtualizado.HoraInicio;
            HoraTermino = registroAtualizado.HoraTermino;
            PeriodoRecuperacao = registroAtualizado.PeriodoRecuperacao;
            Medicos = registroAtualizado.Medicos;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Cirurgia other = (Cirurgia)obj;
            return this.Data == other.Data &&
                   this.HoraInicio == other.HoraInicio &&
                   this.HoraTermino == other.HoraTermino &&
                   this.PeriodoRecuperacao == other.PeriodoRecuperacao &&
                   this.Medicos.SequenceEqual(other.Medicos);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Data, HoraInicio, HoraTermino, PeriodoRecuperacao, Medicos);
        }
    }
}
