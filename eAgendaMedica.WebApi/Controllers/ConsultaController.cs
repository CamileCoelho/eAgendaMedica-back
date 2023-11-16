using eAgendaMedica.WebApi.ViewModels.ModuloAtividade;
using eAgendaMedica.Aplicacao.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.WebApi.Controllers
{
    [Route("api/atividades/consultas")]
    [ApiController]
    public class ConsultaController : ControllerBase
    {
        private readonly ServicoConsulta servicoConsulta;
        private readonly IMapper mapeador;

        public ConsultaController(ServicoConsulta servicoConsulta, IMapper mapeador)
        {
            this.servicoConsulta = servicoConsulta;
            this.mapeador = mapeador;
        }

        [HttpGet("consultas")]
        [ProducesResponseType(typeof(ListarConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodasConsultas()
        {
            var categoriasResult = await servicoConsulta.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarConsultaViewModel>>(categoriasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("consultas/visualiacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarConsultaPorId(Guid id)
        {
            var categoriaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (categoriaResult.IsFailed)
                return NotFound(categoriaResult.Errors);

            var viewModel = mapeador.Map<VisualizarConsultaViewModel>(categoriaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost("consultas")]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> InserirConsulta(FormsConsultaViewModel viewModel)
        {
            var categoria = mapeador.Map<Consulta>(viewModel);

            var categoriaResult = await servicoConsulta.InserirAsync(categoria);

            if (categoriaResult.IsFailed)
                return BadRequest(categoriaResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("consultas/{id}")]
        [ProducesResponseType(typeof(FormsConsultaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> EditarConsulta(Guid id, FormsConsultaViewModel viewModel)
        {
            var selecacaoConsultaResult = await servicoConsulta.SelecionarPorIdAsync(id);

            if (selecacaoConsultaResult.IsFailed)
                return NotFound(selecacaoConsultaResult.Errors);

            var categoria = mapeador.Map(viewModel, selecacaoConsultaResult.Value);

            var categoriaResult = await servicoConsulta.EditarAsync(categoria);

            if (categoriaResult.IsFailed)
                return BadRequest(categoriaResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("consultas/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> ExcluirConsulta(Guid id)
        {
            var categoriaResult = await servicoConsulta.ExcluirAsync(id);

            if (categoriaResult.IsFailed)
                return NotFound(categoriaResult.Errors);

            return Ok();
        }
    }
}