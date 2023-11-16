﻿using eAgendaMedica.WebApi.ViewModels.ModuloAtividade;
using eAgendaMedica.Aplicacao.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.WebApi.Controllers
{
    [Route("api/atividades/cirurgias")]
    [ApiController]
    public class CirurgiaController : ApiControllerBase
    {
        private readonly ServicoCirurgia servicoCirurgia;
        private readonly IMapper mapeador;

        public CirurgiaController(ServicoCirurgia servicoCirurgia, IMapper mapeador)
        {
            this.servicoCirurgia = servicoCirurgia;
            this.mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodasCirurgias()
        {
            var cirurgiasResult = await servicoCirurgia.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarCirurgiaViewModel>>(cirurgiasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualiacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarCirurgiaPorId(Guid id)
        {
            var cirurgiaResult = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (cirurgiaResult.IsFailed)
                return NotFound(cirurgiaResult.Errors);

            var viewModel = mapeador.Map<VisualizarCirurgiaViewModel>(cirurgiaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> InserirCirurgia(FormsCirurgiaViewModel viewModel)
        {
            var cirurgia = mapeador.Map<Cirurgia>(viewModel);

            var cirurgiaResult = await servicoCirurgia.InserirAsync(cirurgia);

            if (cirurgiaResult.IsFailed)
                return BadRequest(cirurgiaResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsCirurgiaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> EditarCirurgia(Guid id, FormsCirurgiaViewModel viewModel)
        {
            var selecacaoCirurgiaResult = await servicoCirurgia.SelecionarPorIdAsync(id);

            if (selecacaoCirurgiaResult.IsFailed)
                return NotFound(selecacaoCirurgiaResult.Errors);

            var medico = mapeador.Map(viewModel, selecacaoCirurgiaResult.Value);

            var mdicoResult = await servicoCirurgia.EditarAsync(medico);

            if (mdicoResult.IsFailed)
                return BadRequest(mdicoResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> ExcluirCirurgia(Guid id)
        {
            var cirurgiaResult = await servicoCirurgia.ExcluirAsync(id);

            if (cirurgiaResult.IsFailed)
                return NotFound(cirurgiaResult.Errors);

            return Ok();
        }
    }
}