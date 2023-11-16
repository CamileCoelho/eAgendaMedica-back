using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.Aplicacao.ModuloAtividade
{
    public class ServicoConsulta : ServicoBase<Consulta, ValidadorConsulta>
    {
        private readonly IRepositorioConsulta repositorioConsulta;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoConsulta(IRepositorioConsulta repositorioConsulta, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioConsulta = repositorioConsulta;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Consulta>> InserirAsync(Consulta consulta)
        {
            var resultadoValidacao = Validar(consulta);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioConsulta.InserirAsync(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> EditarAsync(Consulta consulta)
        {
            var resultadoValidacao = Validar(consulta);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioConsulta.Editar(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> ExcluirAsync(Guid id)
        {
            var consulta = await repositorioConsulta.SelecionarPorIdAsync(id);

            if (consulta == null)
                return Result.Fail("Consulta não encontrado.");

            repositorioConsulta.Excluir(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Consulta>>> SelecionarTodosAsync()
        {
            var consultas = await repositorioConsulta.SelecionarTodosAsync();

            return Result.Ok(consultas);
        }

        public async Task<Result<Consulta>> SelecionarPorIdAsync(Guid id)
        {
            var consulta = await repositorioConsulta.SelecionarPorIdAsync(id);

            if (consulta == null)
                return Result.Fail("Consulta não encontrado.");

            return Result.Ok(consulta);
        }
    }
}
