using eAgendaMedica.WebApi.ViewModels.ModuloAtividade;
using eAgendaMedica.Aplicacao.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.WebApi.Controllers
{
    [Route("api/atividades")]
    [ApiController]
    public class AtividadeController : ApiControllerBase
    {
        private readonly ServicoConsulta servicoConsulta;
        private readonly ServicoCirurgia servicoCirurgia;
        private readonly IMapper mapeador;

        public AtividadeController(ServicoConsulta servicoConsulta, 
                                   ServicoCirurgia servicoCirurgia, 
                                   IMapper mapeador)
        {
            this.servicoConsulta = servicoConsulta;
            this.servicoCirurgia = servicoCirurgia;
            this.mapeador = mapeador;
        }

        [HttpGet("consultas")]
        [ProducesResponseType(typeof(List<ListarConsultaViewModel>), 200)]
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

        [HttpGet("cirurgias")]
        [ProducesResponseType(typeof(List<ListarCirurgiaViewModel>), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodasCirurgias()
        {
            var cirurgiasResult = await servicoCirurgia.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarCirurgiaViewModel>>(cirurgiasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("cirurgias/visualiacao-completa/{id}")]
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

        [HttpPost("cirurgias")]
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

        [HttpPut("cirurgias/{id}")]
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

        [HttpDelete("cirurgias/{id}")]
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