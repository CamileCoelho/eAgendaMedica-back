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
                .ForMember(destino => destino.DataInicio, opt => opt.MapFrom(origem => origem.DataInicio.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom<UsuarioResolver>())
                .AfterMap<ConfigFormsCirurgiaMappingAction>();
        }

        private void ConfigurarMapeamentoEntidadeParaViewModel()
        {
            CreateMap<Cirurgia, ListarCirurgiaViewModel>()
                .ForMember(destino => destino.DataInicio, opt => opt.MapFrom(origem => origem.DataInicio.ToShortDateString()))
                .ForMember(destino => destino.DataTermino, opt => opt.MapFrom(origem => origem.DataTermino.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")));

            CreateMap<Cirurgia, VisualizarCirurgiaViewModel>()
                .ForMember(destino => destino.DataInicio, opt => opt.MapFrom(origem => origem.DataInicio.ToShortDateString()))
                .ForMember(destino => destino.DataTermino, opt => opt.MapFrom(origem => origem.DataTermino.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .ForMember(destino => destino.Medicos, opt => opt.MapFrom(origem => origem.Medicos));

            CreateMap<Cirurgia, FormsCirurgiaViewModel>()
                .ForMember(destino => destino.DataInicio, opt => opt.MapFrom(origem => origem.DataInicio.ToShortDateString()))
                .ForMember(destino => destino.HoraInicio, opt => opt.MapFrom(origem => origem.HoraInicio.ToString(@"hh\:mm")))
                .ForMember(destino => destino.HoraTermino, opt => opt.MapFrom(origem => origem.HoraTermino.ToString(@"hh\:mm")))
                .AfterMap<ConfigCirurgiaMappingAction>();

        }
    }
}