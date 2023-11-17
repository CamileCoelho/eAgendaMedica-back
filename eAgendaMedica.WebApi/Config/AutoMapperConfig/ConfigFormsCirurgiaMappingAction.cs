﻿using eAgendaMedica.Dominio.ModuloAtividade;
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
            destination.Medicos.AddRange(repositorioMedico.SelecionarMuitos(source.MedicoIds));
        }
    }
}