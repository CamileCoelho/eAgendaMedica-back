namespace eAgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class ListarCirurgiaViewModel
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        //public List<ListarMedicoViewModel> Medicos { get; set; }

        //public ListarCirurgiaViewModel()
        //{
        //    Medicos = new List<ListarMedicoViewModel>();
        //}
    }
}
