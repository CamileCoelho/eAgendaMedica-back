using eAgendaMedica.Aplicacao.ModuloAutenticacao;
using eAgendaMedica.WebApi.ViewModels.ModuloAutenticacao;

namespace eAgendaMedica.WebApi.Controllers
{
    [Route("api/contas")]
    [ApiController]
    public class AutenticacaoController : ApiControllerBase
    {
        private readonly ServicoAutenticacao servicoAutenticacao;
        private readonly IMapper mapeador;

        public AutenticacaoController(ServicoAutenticacao servicoAutenticacao, IMapper mapeador)
        {
            this.servicoAutenticacao = servicoAutenticacao;
            this.mapeador = mapeador;
        }


        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(RegistrarUsuarioViewModel usuarioViewModel)
        {
            var usuario = mapeador.Map<Usuario>(usuarioViewModel);

            var usuarioResult = await servicoAutenticacao.RegistrarAsync(usuario, usuarioViewModel.Senha);

            if (usuarioResult.IsFailed)
                return NotFound(usuarioResult.Errors);

            return Ok(usuarioViewModel);
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar(AutenticarUsuarioViewModel usuarioViewModel)
        {
            var usuarioResult = await servicoAutenticacao.AutenticarAsync(usuarioViewModel.Login, usuarioViewModel.Senha);

            if (usuarioResult.IsFailed)
                return NotFound(usuarioResult.Errors);

            return Ok(usuarioViewModel);
        }

        [HttpPost("sair")]
        public async Task<IActionResult> Sair()
        {
            await servicoAutenticacao.SairAsync();

            return Ok();
        }
    }
}
