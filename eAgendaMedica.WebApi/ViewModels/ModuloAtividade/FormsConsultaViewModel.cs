using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class FormsConsultaViewModel
    {
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Guid MedicoId { get; set; }

        public FormsConsultaViewModel()
        {
            MedicoId = new Guid();
        }
    }
}
