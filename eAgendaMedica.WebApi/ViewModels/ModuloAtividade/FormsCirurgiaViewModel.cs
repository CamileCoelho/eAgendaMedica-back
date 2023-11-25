namespace eAgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class FormsCirurgiaViewModel
    {
        public string? Detalhes { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public List<Guid> MedicoIds { get; set; }

        public FormsCirurgiaViewModel()
        {
            MedicoIds = new List<Guid>();
        }
    }
}
