using eAgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class VisualizarCirurgiaViewModel
    {
        public Guid Id { get; set; }
        public string? Detalhes { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public List<ListarMedicoViewModel> Medicos { get; set; }

        public VisualizarCirurgiaViewModel()
        {
            Medicos = new List<ListarMedicoViewModel>();
        }
    }
}
