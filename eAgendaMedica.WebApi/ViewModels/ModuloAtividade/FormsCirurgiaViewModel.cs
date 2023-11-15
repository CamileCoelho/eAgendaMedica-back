using eAgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class FormsCirurgiaViewModel
    {
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public List<ListarMedicoViewModel> Medicos { get; set; }

        public FormsCirurgiaViewModel()
        {
            Medicos = new List<ListarMedicoViewModel>();
        }
    }
}
