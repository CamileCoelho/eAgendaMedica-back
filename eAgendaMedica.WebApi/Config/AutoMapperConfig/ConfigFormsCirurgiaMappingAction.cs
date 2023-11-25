using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloAtividade;

namespace eAgendaMedica.WebApi.Config.AutoMapperConfig
{
    public class ConfigFormsCirurgiaMappingAction : IMappingAction<FormsCirurgiaViewModel, Cirurgia>
    {
        private readonly IRepositorioMedico repositorioMedico;

        public ConfigFormsCirurgiaMappingAction(IRepositorioMedico repositorioMedico)
        {
            this.repositorioMedico = repositorioMedico;
        }

        public void Process(FormsCirurgiaViewModel source, Cirurgia destination, ResolutionContext context)
        {
            destination.Medicos.Clear();

            destination.Medicos.AddRange(repositorioMedico.SelecionarMuitos(source.MedicoIds));
        }
    }

    public class ConfigCirurgiaMappingAction : IMappingAction<Cirurgia, FormsCirurgiaViewModel>
    {
        public void Process(Cirurgia source, FormsCirurgiaViewModel destination, ResolutionContext context)
        {
            var ids = new List<Guid>();

            foreach (var medico in source.Medicos)
            {
                ids.Add(medico.Id);
            }

            destination.MedicoIds = ids;
        }
    }
}