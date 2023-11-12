using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloMedico;
using System.Security.Cryptography;

namespace eAgendaMedica.Dominio.ModuloAtividade
{
    public class Atividade : EntidadeBase<Atividade>
    {
        private TipoDeAtendimentoEnum _tipoDeAtendimento;
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public object EspecificoConsultaOuCirurgia { get; set; }

        public Atividade()
        {
            Data = DateTime.Now;
            HoraInicio = Data.TimeOfDay;
            HoraTermino = Data.TimeOfDay;
        }

        public Atividade(DateTime data, TimeSpan horaInicio, TimeSpan horaTermino,
            TipoDeAtendimentoEnum tipoAtendimento)
        {
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
            TipoDeAtendimento = tipoAtendimento;
        }

        public TipoDeAtendimentoEnum TipoDeAtendimento
        {
            get { return _tipoDeAtendimento; }
            set
            {
                _tipoDeAtendimento = value;

                switch (_tipoDeAtendimento)
                {
                    case TipoDeAtendimentoEnum.Consulta:
                        this.PeriodoRecuperacao = TimeSpan.FromMinutes(20);
                        this.EspecificoConsultaOuCirurgia = new Medico(); 
                        break;

                    case TipoDeAtendimentoEnum.Cirurgia:
                        this.PeriodoRecuperacao = TimeSpan.FromMinutes(240); // 4 horas em minutos
                        this.EspecificoConsultaOuCirurgia = new List<Medico>();
                        break;

                    default:
                        break;
                }
            }
        }

        public override void AtualizarInformacoes(Atividade registroAtualizado)
        {
            Data = registroAtualizado.Data;
            HoraInicio = registroAtualizado.HoraInicio;
            HoraTermino = registroAtualizado.HoraTermino;
            TipoDeAtendimento = registroAtualizado.TipoDeAtendimento;
        }
    }
}
