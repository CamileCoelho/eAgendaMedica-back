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
                .ForMember(destino => destino.DataInicio, opt => opt.MapFrom(origem => origem.DataInicio.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .AfterMap<ConfigFormsConsultaMappingAction>();
        }

        private void ConfigurarMapeamentoEntidadeParaViewModel()
        {
            CreateMap<Consulta, ListarConsultaViewModel>()
                .ForMember(destino => destino.DataInicio, opt => opt.MapFrom(origem => origem.DataInicio.ToShortDateString()))
                .ForMember(destino => destino.DataTermino, opt => opt.MapFrom(origem => origem.DataTermino.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(destino => destino.NomeMedico, opt => opt.MapFrom(destino => destino.Medico.Nome));

            CreateMap<Consulta, VisualizarConsultaViewModel>()
                .ForMember(destino => destino.DataInicio, opt => opt.MapFrom(origem => origem.DataInicio.ToShortDateString()))
                .ForMember(destino => destino.DataTermino, opt => opt.MapFrom(origem => origem.DataTermino.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(destino => destino.Medico, opt => opt.MapFrom(destino => destino.Medico));
        }
    }
}