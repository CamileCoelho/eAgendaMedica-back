namespace eAgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class ListarConsultaViewModel
    {
        public Guid Id { get; set; }
        public string? Detalhes { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public string NomeMedico { get; set; }
    }
}
