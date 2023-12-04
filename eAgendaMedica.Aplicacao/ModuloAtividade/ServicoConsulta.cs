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
            if (consulta != null)
            {
                consulta.DataInicio = consulta.DataInicio.Date + consulta.HoraInicio;
                consulta.DataTermino = consulta.DataTermino.Date + consulta.HoraTermino;
            }

            var resultadoValidacao = Validar(consulta);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            consulta.Medico.Consultas.Add(consulta);

            await repositorioConsulta.InserirAsync(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> EditarAsync(Consulta consulta)
        {
            consulta.DataInicio = consulta.DataInicio.Date + consulta.HoraInicio;
            consulta.DataTermino = consulta.DataTermino.Date + consulta.HoraTermino;

            var resultadoValidacao = Validar(consulta);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            if (!consulta.Medico.Consultas.Contains(consulta))
                consulta.Medico.Consultas.Add(consulta);

            repositorioConsulta.Editar(consulta);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(consulta);
        }

        public async Task<Result<Consulta>> ExcluirAsync(Guid id)
        {
            var consulta = await repositorioConsulta.SelecionarPorIdAsync(id);

            if (consulta == null)
                return Result.Fail("Consulta não encontrada.");

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
                return Result.Fail("Consulta não encontrada.");

            return Result.Ok(consulta);
        }

        protected override Result Validar(Consulta consulta)
        {
            var validador = new ValidadorConsulta();

            var resultadoValidacao = validador.Validate(consulta);

            var erros = new List<Error>();

            if (consulta.Medico.VerificarConflitoHorario(consulta.Medico, consulta.DataInicio, consulta.DataTermino, consulta.Id))
            {
                Log.Logger.Warning($"O médico '{consulta.Medico.Nome}' já possuí uma atividade que conflita com esse período.");

                erros.Add(new Error($"O médico '{consulta.Medico.Nome}' já possuí uma atividade que conflita com esse período."));
            }

            foreach (var validationFailure in resultadoValidacao.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                erros.Add(new Error(validationFailure.ErrorMessage));
            }

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }        
    }
}
