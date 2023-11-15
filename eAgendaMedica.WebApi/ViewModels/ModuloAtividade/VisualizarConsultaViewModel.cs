﻿using eAgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebApi.ViewModels.ModuloAtividade
{
    public class VisualizarConsultaViewModel
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public TimeSpan PeriodoRecuperacao { get; set; }
        public ListarMedicoViewModel Medico { get; set; }

        public VisualizarConsultaViewModel()
        {
            Medico = new ListarMedicoViewModel();
        }
    }
}
