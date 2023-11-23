using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloMedico;
using Microsoft.AspNetCore.Authorization;

namespace eAgendaMedica.WebApi.Controllers
{
    [Route("api/medicos")]
    [ApiController]
    [Authorize]
    public class MedicoController : ApiControllerBase
    {
        private readonly ServicoMedico servicoMedico;
        private readonly IMapper mapeador;

        public MedicoController(ServicoMedico servicoMedico, IMapper mapeador)
        {
            this.servicoMedico = servicoMedico;
            this.mapeador = mapeador;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(FormsMedicoViewModel viewModel)
        {
            var medico = mapeador.Map<Medico>(viewModel);

            var medicoResult = await servicoMedico.InserirAsync(medico);

            if (medicoResult.IsFailed)
                return BadRequest(medicoResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, FormsMedicoViewModel viewModel)
        {
            var selecacaoMedicoResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (selecacaoMedicoResult.IsFailed)
                return NotFound(selecacaoMedicoResult.Errors);

            var medico = mapeador.Map(viewModel, selecacaoMedicoResult.Value);

            var medicoResult = await servicoMedico.EditarAsync(medico);

            if (medicoResult.IsFailed)
                return BadRequest(medicoResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var medicoResult = await servicoMedico.ExcluirAsync(id);

            if (medicoResult.IsFailed)
                return NotFound(medicoResult.Errors);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var medicosResult = await servicoMedico.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarMedicoViewModel>>(medicosResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarCompletoPorId(Guid id)
        {
            var medicoResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (medicoResult.IsFailed)
                return NotFound(medicoResult.Errors);

            var viewModel = mapeador.Map<VisualizarMedicoViewModel>(medicoResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var medicoResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (medicoResult.IsFailed)
                return NotFound(medicoResult.Errors);

            var viewModel = mapeador.Map<FormsMedicoViewModel>(medicoResult.Value);

            return Ok(viewModel);
        }
    }
}