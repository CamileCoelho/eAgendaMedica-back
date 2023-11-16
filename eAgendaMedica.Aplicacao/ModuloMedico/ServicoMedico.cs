using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Aplicacao.ModuloMedico
{
    public class ServicoMedico : ServicoBase<Medico, ValidadorMedico>
    {
        private readonly IRepositorioMedico repositorioMedico;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoMedico(IRepositorioMedico repositorioMedico, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioMedico = repositorioMedico;
            this.contextoPersistencia = contextoPersistencia;
        }


        public async Task<Result<Medico>> InserirAsync(Medico medico)
        {
            var resultadoValidacao = Validar(medico);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioMedico.InserirAsync(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> EditarAsync(Medico medico)
        {
            var resultadoValidacao = Validar(medico);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioMedico.Editar(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(medico);
        }

        public async Task<Result<Medico>> ExcluirAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            if (medico == null)
                return Result.Fail("Médico não encontrado.");

            repositorioMedico.Excluir(medico);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Medico>>> SelecionarTodosAsync()
        {
            var medicos = await repositorioMedico.SelecionarTodosAsync();

            return Result.Ok(medicos);
        }

        public async Task<Result<Medico>> SelecionarPorIdAsync(Guid id)
        {
            var medico = await repositorioMedico.SelecionarPorIdAsync(id);

            if (medico == null)
                return Result.Fail("Médico não encontrado.");

            return Result.Ok(medico);
        }       
    }
}