using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class FormsConsultaViewModel
    {
        public string? Detalhes { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Guid MedicoId { get; set; }

        public FormsConsultaViewModel()
        {
            MedicoId = new Guid();
        }
    }
}
