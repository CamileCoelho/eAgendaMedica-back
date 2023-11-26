using eAgendaMedica.Dominio.ModuloAtividade;
using eAgendaMedica.Dominio.ModuloMedico;

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
            if (cirurgia != null)
            {
                cirurgia.DataInicio = cirurgia.DataInicio.Date + cirurgia.HoraInicio;
                cirurgia.DataTermino = cirurgia.DataTermino.Date + cirurgia.HoraTermino;
            }

            var resultadoValidacao = Validar(cirurgia);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            foreach (var m in cirurgia.Medicos)
            {
                m.Cirurgias.Add(cirurgia);
            }

            await repositorioCirurgia.InserirAsync(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result<Cirurgia>> EditarAsync(Cirurgia cirurgia)
        {
            cirurgia.DataInicio = cirurgia.DataInicio.Date + cirurgia.HoraInicio;
            cirurgia.DataTermino = cirurgia.DataTermino.Date + cirurgia.HoraTermino;

            var resultadoValidacao = Validar(cirurgia);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);
            
            foreach (var m in cirurgia.Medicos)
            {
                if (!m.Cirurgias.Contains(cirurgia))
                    m.Cirurgias.Add(cirurgia);
            }

            repositorioCirurgia.Editar(cirurgia);

            await contextoPersistencia.GravarDadosAsync();

            return Result.Ok(cirurgia);
        }

        public async Task<Result<Cirurgia>> ExcluirAsync(Guid id)
        {
            var cirurgia = await repositorioCirurgia.SelecionarPorIdAsync(id);

            if (cirurgia == null)
                return Result.Fail("Cirurgia não encontrada.");

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
                return Result.Fail("Cirurgia não encontrada.");

            return Result.Ok(cirurgia);
        }

        protected override Result Validar(Cirurgia cirurgia)
        {
            var validador = new ValidadorCirurgia();

            var resultadoValidacao = validador.Validate(cirurgia);

            var erros = new List<Error>();

            Medico medico = null;

            foreach (var m in cirurgia.Medicos)
            {
                if (VerificarConflitoHorario(m, cirurgia.DataInicio, cirurgia.DataTermino, cirurgia.Id))
                    medico = m;
            }

            if (medico != null)
            {
                Log.Logger.Warning($"O médico '{medico.Nome}' já possuí uma atividade que conflita com esse período.");

                erros.Add(new Error($"O médico '{medico.Nome}' já possuí uma atividade que conflita com esse período."));
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