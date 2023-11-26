using eAgendaMedica.Aplicacao.ModuloAutenticacao;
using eAgendaMedica.WebApi.ViewModels.ModuloAutenticacao;

namespace eAgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<RegistrarUsuarioViewModel, Usuario>()
                .ForMember(destino => destino.EmailConfirmed, opt => opt.MapFrom(origem => true))
                .ForMember(destino => destino.UserName, opt => opt.MapFrom(origem => origem.Login));
        }
    }
}
