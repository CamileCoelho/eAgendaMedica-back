using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloAtividade;

namespace eAgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class ConfigFormsConsultaMappingAction : IMappingAction<FormsConsultaViewModel, Consulta>
    {
        private readonly IRepositorioMedico repositorioMedico;

        public ConfigFormsConsultaMappingAction(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }

        public void Process(FormsConsultaViewModel source, Consulta destination, ResolutionContext context)
        {
            destination.Medico = repositorioMedico.SelecionarPorId(source.MedicoId);
        }
    }
}