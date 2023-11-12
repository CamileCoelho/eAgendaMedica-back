using eAgendaMedica.Dominio.Compartilhado;

namespace eAgendaMedica.Dominio.ModuloMedico
{
    public class Medico : EntidadeBase<Medico>
    {
        public string Nome { get; set; }
        public string Crm { get; set; }
        public string Especialidade { get; set; }

        public Medico()
        {
            
        }

        public Medico(string nome, string crm, string especialidade)
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
        }
    }
}
