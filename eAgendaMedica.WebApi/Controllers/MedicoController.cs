using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.WebApi.ViewModels.ModuloMedico;

namespace eAgendaMedica.WebApi.Controllers
{
    [Route("api/medicos")]
    [ApiController]
    public class MedicoController : ApiControllerBase
    {
        private readonly ServicoMedico servicoMedico;
        private readonly IMapper mapeador;

        public MedicoController(ServicoMedico servicoMedico, IMapper mapeador)
        {
            this.servicoMedico = servicoMedico;
            this.mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var categoriasResult = await servicoMedico.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarMedicoViewModel>>(categoriasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualiacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var categoriaResult = await servicoMedico.SelecionarPorIdAsync(id);

            if (categoriaResult.IsFailed)
                return NotFound(categoriaResult.Errors);

            var viewModel = mapeador.Map<VisualizarMedicoViewModel>(categoriaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormsMedicoViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(FormsMedicoViewModel viewModel)
        {
            var categoria = mapeador.Map<Medico>(viewModel);

            var categoriaResult = await servicoMedico.InserirAsync(categoria);

            if (categoriaResult.IsFailed)
                return BadRequest(categoriaResult.Errors);

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

            var categoria = mapeador.Map(viewModel, selecacaoMedicoResult.Value);

            var categoriaResult = await servicoMedico.EditarAsync(categoria);

            if (categoriaResult.IsFailed)
                return BadRequest(categoriaResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var categoriaResult = await servicoMedico.ExcluirAsync(id);

            if (categoriaResult.IsFailed)
                return NotFound(categoriaResult.Errors);

            return Ok();
        }
    }
}