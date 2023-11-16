using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.Aplicacao.ModuloAtividade
{
    public class ServicoCirurgia : ServicoBase<Cirurgia, ValidadorCirurgia>
    {
        private readonly IRepositorioCirurgia repositorioCirurgia;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoCirurgia(IRepositorioCirurgia repositorioCirurgia, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioCirurgia = repositorioCirurgia;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Cirurgia>> InserirAsync(Cirurgia cirurgia)
        {
            var resultadoValidacao = Validar(cirurgia);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioCirurgia.InserirAsync(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result<Cirurgia>> EditarAsync(Cirurgia cirurgia)
        {
            var resultadoValidacao = Validar(cirurgia);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioCirurgia.Editar(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result<Cirurgia>> ExcluirAsync(Guid id)
        {
            var cirurgia = await repositorioCirurgia.SelecionarPorIdAsync(id);

            if (cirurgia == null)
                return Result.Fail("Cirurgia não encontrado.");

            repositorioCirurgia.Excluir(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Cirurgia>>> SelecionarTodosAsync()
        {
            var cirurgias = await repositorioCirurgia.SelecionarTodosAsync();

            return Result.Ok(cirurgias);
        }

        public async Task<Result<Cirurgia>> SelecionarPorIdAsync(Guid id)
        {
            var cirurgia = await repositorioCirurgia.SelecionarPorIdAsync(id);

            if (cirurgia == null)
                return Result.Fail("Cirurgia não encontrado.");

            return Result.Ok(cirurgia);
        }
    }
}