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
