using eAgendaMedica.Aplicacao.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.WebApi.ViewModels.ModuloAtividade;
using Microsoft.AspNetCore.Authorization;

namespace eAgendaMedica.WebApi.Controllers
{
    [Route("api/atividades/consultas")]
    [ApiController]
    [Authorize]
    public class ConsultaController : ApiControllerBase
    {
        private readonly ServicoConsulta servicoConsulta;
        private readonly IMapper mapeador;

        public ConsultaController(ServicoConsulta servicoConsulta, IMapper mapeador)
        {
            this.servicoConsulta = servicoConsulta;
            this.mapeador = mapeador;
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> InserirAsync(FormsConsultaViewModel viewModel)
        {
            var consulta = mapeador.Map<Consulta>(viewModel);

            var consultaResult = await servicoConsulta.InserirAsync(consulta);

            if (consultaResult.IsFailed)
                return BadRequest(consultaResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> EditarAsync(Guid id, FormsConsultaViewModel viewModel)
        {
            var selecacaoConsultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (selecacaoConsultaResult.IsFailed)
                return NotFound(selecacaoConsultaResult.Errors);

            var consulta = mapeador.Map(viewModel, selecacaoConsultaResult.Value);

            var consultaResult = await servicoConsulta.EditarAsync(consulta);

            if (consultaResult.IsFailed)
                return BadRequest(consultaResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> ExcluirAsync(Guid id)
        {
            var consultaResult = await servicoConsulta.ExcluirAsync(id);

            if (consultaResult.IsFailed)
                return NotFound(consultaResult.Errors);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodas()
        {
            var consultasResult = await servicoConsulta.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarConsultaViewModel>>(consultasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var consultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (consultaResult.IsFailed)
                return NotFound(consultaResult.Errors);

            var viewModel = mapeador.Map<FormsConsultaViewModel>(consultaResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualizacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarCompletaPorId(Guid id)
        {
            var consultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (consultaResult.IsFailed)
                return NotFound(consultaResult.Errors);

            var viewModel = mapeador.Map<VisualizarConsultaViewModel>(consultaResult.Value);

            return Ok(viewModel);
        }
    }
}