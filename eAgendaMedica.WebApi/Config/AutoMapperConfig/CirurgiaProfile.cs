using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.WebApi.ViewModels.ModuloAtividade;

namespace eAgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class CirurgiaProfile : Profile
    {
        public CirurgiaProfile()
        {
            ConfigurarMapeamentoViewModelsParaEntidades();

            ConfigurarMapeamentoEntidadeParaViewModel();
        }

        private void ConfigurarMapeamentoViewModelsParaEntidades()
        {
            CreateMap<FormsCirurgiaViewModel, Cirurgia>()
                .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .AfterMap<ConfigFormsCirurgiaMappingAction>();
        }

        private void ConfigurarMapeamentoEntidadeParaViewModel()
        {
            CreateMap<Cirurgia, ListarCirurgiaViewModel>()
                .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<Cirurgia, VisualizarCirurgiaViewModel>()
                .ForMember(destino => destino.Data, opt => opt.MapFrom(origem => origem.Data.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));
        }
    }
}