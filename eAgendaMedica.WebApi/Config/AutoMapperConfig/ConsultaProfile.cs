using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.WebApi.ViewModels.ModuloAtividade;

namespace eAgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class ConsultaProfile : Profile
    {
        public ConsultaProfile()
        {
            ConfigurarMapeamentoViewModelsParaEntidades();

            ConfigurarMapeamentoEntidadeParaViewModel();
        }

        private void ConfigurarMapeamentoViewModelsParaEntidades()
        {
            CreateMap<FormsConsultaViewModel, Consulta>()
                .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(destino => destino.Medico.Id, opt => opt.MapFrom(destino => destino.MedicoId));
        }

        private void ConfigurarMapeamentoEntidadeParaViewModel()
        {
            CreateMap<Consulta, ListarConsultaViewModel>()
                .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(destino => destino.NomeMedico, opt => opt.MapFrom(destino => destino.Medico.Nome));

            CreateMap<Consulta, VisualizarConsultaViewModel>()
                .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(destino => destino.Medico, opt => opt.MapFrom(destino => destino.Medico));
        }
    }
}