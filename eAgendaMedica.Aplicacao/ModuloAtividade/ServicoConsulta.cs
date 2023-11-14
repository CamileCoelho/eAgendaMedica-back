using eAgendaMedica.Dominio.ModuloAtividade;

namespace eAgendaMedica.Aplicacao.ModuloAtividade
{
    public class ServicoConsulta
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
            var resultadoValidacao = ValidarConsulta(consulta);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioConsulta.InserirAsync(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> EditarAsync(Consulta consulta)
        {
            var resultadoValidacao = ValidarConsulta(consulta);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioConsulta.Editar(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> ExcluirAsync(Guid id)
        {
            var consulta = await repositorioConsulta.SelecionarPorIdAsync(id);

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

            return Result.Ok(consulta);
        }


        private Result ValidarConsulta(Consulta consulta)
        {
            ValidadorConsulta validador = new ValidadorConsulta();

            var resultadoValidacao = validador.Validate(consulta);

            List<Error> erros = new List<Error>();

            foreach (var erro in resultadoValidacao.Errors)
                erros.Add(new Error(erro.ErrorMessage));

            if (erros.Any())
                return Result.Fail(erros.ToArray());

            return Result.Ok();
        }
    }
}
