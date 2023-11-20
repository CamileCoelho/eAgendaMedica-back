using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class MedicoProfile : Profile
    {
        public MedicoProfile()
        {
            CreateMap<FormsMedicoViewModel, Medico>()
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom<UsuarioResolver>());

            CreateMap<Medico, ListarMedicoViewModel>();

            CreateMap<Medico, VisualizarMedicoViewModel>();
        }
    }
}
