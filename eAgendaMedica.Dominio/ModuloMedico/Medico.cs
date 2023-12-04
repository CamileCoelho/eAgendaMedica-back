using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        public string Nome { get; set; }
        public string Crm { get; set; }
        public string Especialidade { get; set; }
        public List<Cirurgia> Cirurgias { get; set; }
        public List<Consulta> Consultas { get; set; }

        public Medico()
        {
            Consultas = new();
            Cirurgias = new();
        }

        public Medico(string nome, string crm, string especialidade) : base()
        {
            Nome = nome;
            Crm = crm;  
            Especialidade = especialidade;
        }

        public bool VerificarConflitoHorario(Medico medico, DateTime novoInicio, DateTime novoTermino, Guid id)
        {
            foreach (var consulta in medico.Consultas)
            {
                DateTime termino = consulta.DataTermino.AddMinutes(20);
                DateTime terminoNovo = novoTermino.AddMinutes(20);

                if (consulta.Id != id && consulta.Medico == medico &&
                   ((novoInicio >= consulta.DataInicio && novoInicio <= termino) ||
                   (terminoNovo >= consulta.DataInicio && terminoNovo <= termino)))
                {
                    return true;
                }
            }

            foreach (var cirurgia in medico.Cirurgias)
            {
                DateTime termino = cirurgia.DataTermino.AddHours(4);
                DateTime terminoNovo = novoTermino.AddHours(4);

                if (cirurgia.Id != id && cirurgia.Medicos.Contains(medico) &&
                   ((novoInicio >= cirurgia.DataInicio && novoInicio <= termino) ||
                   (terminoNovo >= cirurgia.DataInicio && terminoNovo <= termino)))
                {
                    return true;
                }
            }

            return false;
        }

        public override void AtualizarInformacoes(Medico registroAtualizado)
        {
            Nome = registroAtualizado.Nome;
            Crm = registroAtualizado.Crm;
            Especialidade = registroAtualizado.Especialidade;
            Cirurgias = registroAtualizado.Cirurgias;
            Consultas = registroAtualizado.Consultas;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Medico other = (Medico)obj;
            return this.Nome == other.Nome && this.Crm == other.Crm && this.Especialidade == other.Especialidade &&
                Enumerable.SequenceEqual(this.Cirurgias, other.Cirurgias);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nome, Crm, Especialidade);
        }
    }
}
